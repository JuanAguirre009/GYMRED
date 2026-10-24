using GymRed.Application.DTOs;

namespace GymRed.Application.Interfaces.Services;

public interface IClienteService
{
    Task<IReadOnlyCollection<ClienteDto>> ObtenerTodosAsync();
    Task<ClienteDto?> ObtenerPorIdAsync(int id);
    Task<int> CrearAsync(CrearClienteDto dto);
    Task ActualizarAsync(int id, ActualizarClienteDto dto);
    Task CambiarEstadoAsync(int id, string estado);
}
