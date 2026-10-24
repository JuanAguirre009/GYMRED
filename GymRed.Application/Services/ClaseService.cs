using GymRed.Application.DTOs;
using GymRed.Application.Exceptions;
using GymRed.Application.Interfaces.Repositories;
using GymRed.Application.Interfaces.Services;
using GymRed.Application.Mappings;
using GymRed.Domain.Entities;

namespace GymRed.Application.Services;

public class ClaseService : IClaseService
{
    private readonly IClaseRepository _claseRepository;
    private readonly ISesionClaseRepository _sesionRepository;
    private readonly IInstructorRepository _instructorRepository;
    private readonly ISedeRepository _sedeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ClaseService(
        IClaseRepository claseRepository,
        ISesionClaseRepository sesionRepository,
        IInstructorRepository instructorRepository,
        ISedeRepository sedeRepository,
        IUnitOfWork unitOfWork)
    {
        _claseRepository = claseRepository ?? throw new ArgumentNullException(nameof(claseRepository));
        _sesionRepository = sesionRepository ?? throw new ArgumentNullException(nameof(sesionRepository));
        _instructorRepository = instructorRepository ?? throw new ArgumentNullException(nameof(instructorRepository));
        _sedeRepository = sedeRepository ?? throw new ArgumentNullException(nameof(sedeRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<IReadOnlyCollection<ClaseDto>> ObtenerTodasAsync()
    {
        IReadOnlyCollection<Clase> clases = await _claseRepository.ObtenerTodosAsync();
        return clases.Select(c => c.ToDto()).ToList().AsReadOnly();
    }

    public async Task<ClaseDto?> ObtenerPorIdAsync(int id)
    {
        Clase? clase = await _claseRepository.ObtenerPorIdAsync(id);
        return clase?.ToDto();
    }

    public async Task<int> CrearAsync(CrearClaseDto dto)
    {
        if (dto is null)
            throw new AplicacionException("Los datos de la clase son obligatorios.");

        Instructor? instructor = await _instructorRepository.ObtenerPorIdAsync(dto.InstructorId)
            ?? throw new AplicacionException("El instructor asociado no existe.");

        Sede? sede = await _sedeRepository.ObtenerPorIdAsync(dto.SedeId)
            ?? throw new AplicacionException("La sede asociada no existe.");

        Clase clase = new(
            dto.Nombre,
            dto.Descripcion,
            dto.InstructorId,
            dto.SedeId,
            dto.CapacidadMaxima);

        await _claseRepository.AgregarAsync(clase);
        await _unitOfWork.SaveChangesAsync();

        return clase.Id;
    }

    public async Task ActualizarAsync(int id, CrearClaseDto dto)
    {
        if (dto is null)
            throw new AplicacionException("Los datos de la clase son obligatorios.");

        Instructor? instructor = await _instructorRepository.ObtenerPorIdAsync(dto.InstructorId)
            ?? throw new AplicacionException("El instructor asociado no existe.");

        Sede? sede = await _sedeRepository.ObtenerPorIdAsync(dto.SedeId)
            ?? throw new AplicacionException("La sede asociada no existe.");

        Clase? clase = await _claseRepository.ObtenerPorIdAsync(id)
            ?? throw new AplicacionException("Clase no encontrada.");

        clase.Actualizar(
            dto.Nombre,
            dto.Descripcion,
            dto.InstructorId,
            dto.SedeId,
            dto.CapacidadMaxima);

        await _claseRepository.ActualizarAsync(clase);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task CambiarEstadoAsync(int id, bool activo)
    {
        Clase? clase = await _claseRepository.ObtenerPorIdAsync(id)
            ?? throw new AplicacionException("Clase no encontrada.");

        clase.CambiarEstado(activo);
        await _claseRepository.ActualizarAsync(clase);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<SesionClaseDto> ProgramarSesionAsync(ProgramarSesionDto dto)
    {
        if (dto is null)
            throw new AplicacionException("Los datos de la sesion son obligatorios.");

        Clase? clase = await _claseRepository.ObtenerPorIdAsync(dto.ClaseId)
            ?? throw new AplicacionException("Clase no encontrada.");

        SesionClase sesion = clase.ProgramarSesion(dto.FechaHoraInicio, dto.FechaHoraFin);
        await _sesionRepository.AgregarAsync(sesion);
        await _unitOfWork.SaveChangesAsync();

        return sesion.ToDto(0);
    }

    public async Task<IReadOnlyCollection<SesionClaseDto>> ObtenerSesionesPorClaseAsync(int claseId)
    {
        IReadOnlyCollection<SesionClase> sesiones = await _sesionRepository.ObtenerPorClaseAsync(claseId);
        List<SesionClaseDto> resultado = new();

        foreach (SesionClase sesion in sesiones)
        {
            int reservas = await _sesionRepository.ContarReservasActivasAsync(sesion.Id);
            resultado.Add(sesion.ToDto(reservas));
        }

        return resultado.AsReadOnly();
    }
}
