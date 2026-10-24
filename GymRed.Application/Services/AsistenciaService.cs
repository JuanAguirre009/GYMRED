using GymRed.Application.DTOs;
using GymRed.Application.Exceptions;
using GymRed.Application.Interfaces.Repositories;
using GymRed.Application.Interfaces.Services;
using GymRed.Application.Mappings;
using GymRed.Domain.Entities;
using GymRed.Domain.Enums;
using GymRed.Domain.Exceptions;

namespace GymRed.Application.Services;

public class AsistenciaService : IAsistenciaService
{
    private readonly IAsistenciaRepository _asistenciaRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly ISesionClaseRepository _sesionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AsistenciaService(
        IAsistenciaRepository asistenciaRepository,
        IClienteRepository clienteRepository,
        ISesionClaseRepository sesionRepository,
        IUnitOfWork unitOfWork)
    {
        _asistenciaRepository = asistenciaRepository ?? throw new ArgumentNullException(nameof(asistenciaRepository));
        _clienteRepository = clienteRepository ?? throw new ArgumentNullException(nameof(clienteRepository));
        _sesionRepository = sesionRepository ?? throw new ArgumentNullException(nameof(sesionRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<IReadOnlyCollection<AsistenciaDto>> ObtenerPorClienteAsync(int clienteId)
    {
        IReadOnlyCollection<Asistencia> asistencias = await _asistenciaRepository.ObtenerPorClienteAsync(clienteId);
        return asistencias.Select(a => a.ToDto()).ToList().AsReadOnly();
    }

    public async Task<IReadOnlyCollection<AsistenciaDto>> ObtenerPorSesionAsync(int sesionClaseId)
    {
        IReadOnlyCollection<Asistencia> asistencias = await _asistenciaRepository.ObtenerPorSesionAsync(sesionClaseId);
        return asistencias.Select(a => a.ToDto()).ToList().AsReadOnly();
    }

    public async Task<AsistenciaDto> RegistrarAsync(RegistrarAsistenciaDto dto)
    {
        if (dto is null)
            throw new AplicacionException("Los datos de asistencia son obligatorios.");

        Cliente? cliente = await _clienteRepository.ObtenerPorIdAsync(dto.ClienteId)
            ?? throw new AplicacionException("Cliente no encontrado.");

        if (cliente.Estado != EstadoCliente.Activo)
            throw new ReglaNegocioException("El cliente no se encuentra activo.");

        SesionClase? sesion = await _sesionRepository.ObtenerPorIdAsync(dto.SesionClaseId)
            ?? throw new AplicacionException("Sesi�n de clase no encontrada.");

        if (sesion.Estado != EstadoSesionClase.Programada)
            throw new ReglaNegocioException("La asistencia solo puede registrarse para una sesi�n programada.");

        bool yaRegistrada = await _asistenciaRepository.ExisteAsistenciaAsync(dto.SesionClaseId, dto.ClienteId);
        if (yaRegistrada)
            throw new ReglaNegocioException("La asistencia del cliente ya fue registrada para esta sesi�n.");

        Asistencia asistencia = new(dto.SesionClaseId, dto.ClienteId, dto.Asistio);
        await _asistenciaRepository.AgregarAsync(asistencia);
        await _unitOfWork.SaveChangesAsync();

        return asistencia.ToDto();
    }
}
