using GymRed.Application.DTOs;
using GymRed.Application.Exceptions;
using GymRed.Application.Interfaces.Repositories;
using GymRed.Application.Interfaces.Services;
using GymRed.Application.Mappings;
using GymRed.Domain.Entities;
using GymRed.Domain.Enums;
using GymRed.Domain.Exceptions;

namespace GymRed.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ClienteService(IClienteRepository clienteRepository, IUnitOfWork unitOfWork)
    {
        _clienteRepository = clienteRepository ?? throw new ArgumentNullException(nameof(clienteRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<IReadOnlyCollection<ClienteDto>> ObtenerTodosAsync()
    {
        IReadOnlyCollection<Cliente> clientes = await _clienteRepository.ObtenerTodosAsync();
        return clientes.Select(c => c.ToDto()).ToList().AsReadOnly();
    }

    public async Task<ClienteDto?> ObtenerPorIdAsync(int id)
    {
        Cliente? cliente = await _clienteRepository.ObtenerPorIdAsync(id);
        return cliente?.ToDto();
    }

    public async Task<int> CrearAsync(CrearClienteDto dto)
    {
        if (dto is null)
            throw new AplicacionException("Los datos del cliente son obligatorios.");

        bool existe = await _clienteRepository.ExisteIdentificacionAsync(dto.NumeroIdentificacion);
        if (existe)
            throw new ReglaNegocioException("Ya existe un cliente con la misma identificaci�n.");

        Cliente cliente = new(
            dto.Nombres,
            dto.Apellidos,
            dto.NumeroIdentificacion,
            dto.Email,
            dto.Telefono,
            dto.FechaNacimiento,
            dto.SedeId);

        await _clienteRepository.AgregarAsync(cliente);
        await _unitOfWork.SaveChangesAsync();

        return cliente.Id;
    }

    public async Task ActualizarAsync(int id, ActualizarClienteDto dto)
    {
        if (dto is null)
            throw new AplicacionException("Los datos de actualizaci�n son obligatorios.");

        Cliente? cliente = await _clienteRepository.ObtenerPorIdAsync(id)
            ?? throw new AplicacionException("Cliente no encontrado.");

        cliente.ActualizarDatos(dto.Nombres, dto.Apellidos, dto.Email, dto.Telefono);
        await _clienteRepository.ActualizarAsync(cliente);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task CambiarEstadoAsync(int id, string estado)
    {
        if (!Enum.TryParse<EstadoCliente>(estado, true, out EstadoCliente nuevoEstado))
            throw new AplicacionException($"El estado '{estado}' no es v�lido.");

        Cliente? cliente = await _clienteRepository.ObtenerPorIdAsync(id)
            ?? throw new AplicacionException("Cliente no encontrado.");

        cliente.CambiarEstado(nuevoEstado);
        await _clienteRepository.ActualizarAsync(cliente);
        await _unitOfWork.SaveChangesAsync();
    }
}
