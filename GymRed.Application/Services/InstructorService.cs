using GymRed.Application.DTOs;
using GymRed.Application.Exceptions;
using GymRed.Application.Interfaces.Repositories;
using GymRed.Application.Interfaces.Services;
using GymRed.Application.Mappings;
using GymRed.Domain.Entities;

namespace GymRed.Application.Services;

public class InstructorService : IInstructorService
{
    private readonly IInstructorRepository _instructorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InstructorService(IInstructorRepository instructorRepository, IUnitOfWork unitOfWork)
    {
        _instructorRepository = instructorRepository ?? throw new ArgumentNullException(nameof(instructorRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<IReadOnlyCollection<InstructorDto>> ObtenerTodosAsync()
    {
        IReadOnlyCollection<Instructor> instructores = await _instructorRepository.ObtenerTodosAsync();
        return instructores.Select(i => i.ToDto()).ToList().AsReadOnly();
    }

    public async Task<InstructorDto?> ObtenerPorIdAsync(int id)
    {
        Instructor? instructor = await _instructorRepository.ObtenerPorIdAsync(id);
        return instructor?.ToDto();
    }

    public async Task<int> CrearAsync(CrearInstructorDto dto)
    {
        if (dto is null)
            throw new AplicacionException("Los datos del instructor son obligatorios.");

        Instructor instructor = new(dto.Nombres, dto.Apellidos, dto.Email, dto.Telefono, dto.Especialidad);
        await _instructorRepository.AgregarAsync(instructor);
        await _unitOfWork.SaveChangesAsync();

        return instructor.Id;
    }

    public async Task ActualizarAsync(int id, CrearInstructorDto dto)
    {
        if (dto is null)
            throw new AplicacionException("Los datos del instructor son obligatorios.");

        Instructor? instructor = await _instructorRepository.ObtenerPorIdAsync(id)
            ?? throw new AplicacionException("Instructor no encontrado.");

        instructor.ActualizarDatos(dto.Nombres, dto.Apellidos, dto.Email, dto.Telefono, dto.Especialidad);
        await _instructorRepository.ActualizarAsync(instructor);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task CambiarEstadoAsync(int id, bool activo)
    {
        Instructor? instructor = await _instructorRepository.ObtenerPorIdAsync(id)
            ?? throw new AplicacionException("Instructor no encontrado.");

        instructor.CambiarEstado(activo);
        await _instructorRepository.ActualizarAsync(instructor);
        await _unitOfWork.SaveChangesAsync();
    }
}
