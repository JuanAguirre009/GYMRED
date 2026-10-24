using GymRed.Application.DTOs;
using GymRed.Application.Exceptions;
using GymRed.Application.Interfaces.Repositories;
using GymRed.Application.Interfaces.Services;
using GymRed.Application.Mappings;
using GymRed.Domain.Entities;
using GymRed.Domain.Enums;
using GymRed.Domain.ValueObjects;

namespace GymRed.Application.Services;

public class PagoService : IPagoService
{
    private readonly IPagoRepository _pagoRepository;
    private readonly IMembresiaRepository _membresiaRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PagoService(
        IPagoRepository pagoRepository,
        IMembresiaRepository membresiaRepository,
        IClienteRepository clienteRepository,
        IUnitOfWork unitOfWork)
    {
        _pagoRepository = pagoRepository ?? throw new ArgumentNullException(nameof(pagoRepository));
        _membresiaRepository = membresiaRepository ?? throw new ArgumentNullException(nameof(membresiaRepository));
        _clienteRepository = clienteRepository ?? throw new ArgumentNullException(nameof(clienteRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<IReadOnlyCollection<PagoDto>> ObtenerTodosAsync()
    {
        IReadOnlyCollection<Pago> pagos = await _pagoRepository.ObtenerTodosAsync();
        return pagos.Select(p => p.ToDto()).ToList().AsReadOnly();
    }

    public async Task<IReadOnlyCollection<PagoDto>> ObtenerPorMembresiaAsync(int membresiaId)
    {
        IReadOnlyCollection<Pago> pagos = await _pagoRepository.ObtenerPorMembresiaAsync(membresiaId);
        return pagos.Select(p => p.ToDto()).ToList().AsReadOnly();
    }

    public async Task<PagoDto> RegistrarAsync(RegistrarPagoDto dto)
    {
        if (dto is null)
            throw new AplicacionException("Los datos del pago son obligatorios.");

        Membresia? membresia = await _membresiaRepository.ObtenerPorIdAsync(dto.MembresiaId)
            ?? throw new AplicacionException("Membresia no encontrada.");

        if (!Enum.TryParse<MetodoPago>(dto.MetodoPago, true, out MetodoPago metodo))
            throw new AplicacionException($"El metodo de pago '{dto.MetodoPago}' no es valido.");

        Pago pago = membresia.RegistrarPago(new Dinero(dto.Monto, dto.Moneda), metodo, dto.NumeroTransaccion);
        await _pagoRepository.AgregarAsync(pago);
        await _unitOfWork.SaveChangesAsync();

        return pago.ToDto();
    }

    public async Task AnularAsync(int id, string motivo)
    {
        Pago? pago = await _pagoRepository.ObtenerPorIdAsync(id)
            ?? throw new AplicacionException("Pago no encontrado.");

        pago.Anular(motivo);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> ClienteEstaAlDiaAsync(int clienteId)
    {
        Cliente? cliente = await _clienteRepository.ObtenerPorIdAsync(clienteId)
            ?? throw new AplicacionException("Cliente no encontrado.");

        return cliente.TieneMembresiaVigente(DateTime.UtcNow.Date);
    }
}
