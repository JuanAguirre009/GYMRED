using GymRed.Application.DTOs;
using GymRed.Application.Exceptions;
using GymRed.Application.Interfaces.Repositories;
using GymRed.Application.Interfaces.Services;
using GymRed.Application.Mappings;
using GymRed.Domain.Entities;
using GymRed.Domain.Exceptions;

namespace GymRed.Application.Services;

public class MembresiaService : IMembresiaService
{
    private readonly IMembresiaRepository _membresiaRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IPlanRepository _planRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MembresiaService(
        IMembresiaRepository membresiaRepository,
        IClienteRepository clienteRepository,
        IPlanRepository planRepository,
        IUnitOfWork unitOfWork)
    {
        _membresiaRepository = membresiaRepository ?? throw new ArgumentNullException(nameof(membresiaRepository));
        _clienteRepository = clienteRepository ?? throw new ArgumentNullException(nameof(clienteRepository));
        _planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<MembresiaDto?> ObtenerPorIdAsync(int id)
    {
        Membresia? membresia = await _membresiaRepository.ObtenerPorIdAsync(id);
        return membresia?.ToDto();
    }

    public async Task<IReadOnlyCollection<MembresiaDto>> ObtenerPorClienteAsync(int clienteId)
    {
        IReadOnlyCollection<Membresia> membresias = await _membresiaRepository.ObtenerPorClienteAsync(clienteId);
        return membresias.Select(m => m.ToDto()).ToList().AsReadOnly();
    }

    public async Task<IReadOnlyCollection<MembresiaDto>> ObtenerActivasAsync()
    {
        IReadOnlyCollection<Membresia> membresias = await _membresiaRepository.ObtenerActivasAsync();
        return membresias.Select(m => m.ToDto()).ToList().AsReadOnly();
    }

    public async Task<IReadOnlyCollection<MembresiaDto>> ObtenerVencidasAsync()
    {
        IReadOnlyCollection<Membresia> membresias = await _membresiaRepository.ObtenerVencidasAsync();
        return membresias.Select(m => m.ToDto()).ToList().AsReadOnly();
    }

    public async Task<MembresiaDto> AsignarAsync(AsignarMembresiaDto dto)
    {
        if (dto is null)
            throw new AplicacionException("Los datos de asignaci�n son obligatorios.");

        Cliente? cliente = await _clienteRepository.ObtenerPorIdAsync(dto.ClienteId)
            ?? throw new AplicacionException("Cliente no encontrado.");

        Plan? plan = await _planRepository.ObtenerPorIdAsync(dto.PlanId)
            ?? throw new AplicacionException("Plan no encontrado.");

        Membresia membresia = cliente.AsignarMembresia(plan);
        await _membresiaRepository.AgregarAsync(membresia);
        await _unitOfWork.SaveChangesAsync();

        return membresia.ToDto();
    }

    public async Task CancelarAsync(int id)
    {
        Membresia? membresia = await _membresiaRepository.ObtenerPorIdAsync(id)
            ?? throw new AplicacionException("Membres�a no encontrada.");

        membresia.Cancelar();
        await _membresiaRepository.ActualizarAsync(membresia);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> ClienteTieneMembresiaVigenteAsync(int clienteId)
    {
        Cliente? cliente = await _clienteRepository.ObtenerPorIdAsync(clienteId)
            ?? throw new AplicacionException("Cliente no encontrado.");

        return cliente.TieneMembresiaVigente(DateTime.UtcNow.Date);
    }
}
