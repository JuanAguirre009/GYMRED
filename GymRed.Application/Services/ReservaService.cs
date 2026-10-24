using GymRed.Application.DTOs;
using GymRed.Application.Exceptions;
using GymRed.Application.Interfaces.Repositories;
using GymRed.Application.Interfaces.Services;
using GymRed.Application.Mappings;
using GymRed.Domain.Entities;
using GymRed.Domain.Enums;
using GymRed.Domain.Exceptions;

namespace GymRed.Application.Services;

public class ReservaService : IReservaService
{
    private readonly IReservaRepository _reservaRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly ISesionClaseRepository _sesionRepository;
    private readonly IMembresiaRepository _membresiaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReservaService(
        IReservaRepository reservaRepository,
        IClienteRepository clienteRepository,
        ISesionClaseRepository sesionRepository,
        IMembresiaRepository membresiaRepository,
        IUnitOfWork unitOfWork)
    {
        _reservaRepository = reservaRepository ?? throw new ArgumentNullException(nameof(reservaRepository));
        _clienteRepository = clienteRepository ?? throw new ArgumentNullException(nameof(clienteRepository));
        _sesionRepository = sesionRepository ?? throw new ArgumentNullException(nameof(sesionRepository));
        _membresiaRepository = membresiaRepository ?? throw new ArgumentNullException(nameof(membresiaRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<IReadOnlyCollection<ReservaDto>> ObtenerPorClienteAsync(int clienteId)
    {
        IReadOnlyCollection<Reserva> reservas = await _reservaRepository.ObtenerPorClienteAsync(clienteId);
        return reservas.Select(r => r.ToDto()).ToList().AsReadOnly();
    }

    public async Task<IReadOnlyCollection<ReservaDto>> ObtenerPorSesionAsync(int sesionClaseId)
    {
        IReadOnlyCollection<Reserva> reservas = await _reservaRepository.ObtenerPorSesionAsync(sesionClaseId);
        return reservas.Select(r => r.ToDto()).ToList().AsReadOnly();
    }

    public async Task<ReservaDto> CrearAsync(CrearReservaDto dto)
    {
        if (dto is null)
            throw new AplicacionException("Los datos de la reserva son obligatorios.");

        Cliente? cliente = await _clienteRepository.ObtenerPorIdAsync(dto.ClienteId)
            ?? throw new AplicacionException("Cliente no encontrado.");

        if (cliente.Estado != EstadoCliente.Activo)
            throw new ReglaNegocioException("El cliente no se encuentra activo.");

        SesionClase? sesion = await _sesionRepository.ObtenerPorIdAsync(dto.SesionClaseId)
            ?? throw new AplicacionException("Sesi�n de clase no encontrada.");

        if (!sesion.EstaProgramada())
            throw new ReglaNegocioException("La sesi�n no est� disponible para reservas.");

        if (!cliente.TieneMembresiaVigente(DateTime.UtcNow.Date))
            throw new ReglaNegocioException("El cliente no tiene una membres�a vigente.");

        bool existeReservaDuplicada = await _reservaRepository.ExisteReservaActivaAsync(dto.SesionClaseId, dto.ClienteId);
        if (existeReservaDuplicada)
            throw new ReglaNegocioException("El cliente ya tiene una reserva activa para esta sesi�n.");

        int reservasActivas = await _sesionRepository.ContarReservasActivasAsync(dto.SesionClaseId);
        if (!sesion.HayCuposDisponibles(reservasActivas))
            throw new ReglaNegocioException("No hay cupos disponibles para esta sesi�n.");

        Reserva reserva = new(dto.SesionClaseId, dto.ClienteId);
        await _reservaRepository.AgregarAsync(reserva);
        await _unitOfWork.SaveChangesAsync();

        return reserva.ToDto();
    }

    public async Task CancelarAsync(int id)
    {
        Reserva? reserva = await _reservaRepository.ObtenerPorIdAsync(id)
            ?? throw new AplicacionException("Reserva no encontrada.");

        reserva.Cancelar();
        await _reservaRepository.ActualizarAsync(reserva);
        await _unitOfWork.SaveChangesAsync();
    }
}
