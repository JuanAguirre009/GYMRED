using GymRed.Domain.Entities;

namespace GymRed.Application.Interfaces;

public interface IClienteService
{
    Task<IEnumerable<Cliente>> ObtenerTodosAsync();
    Task<Cliente?> ObtenerPorIdAsync(int id);
    Task<int> CrearAsync(Cliente cliente);
    Task ActualizarAsync(Cliente cliente);
    Task CambiarEstadoAsync(int id, bool activo);
}
