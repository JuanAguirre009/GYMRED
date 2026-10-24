using GymRed.Domain.Entities;

namespace GymRed.Application.Interfaces.Repositories;

public interface IClienteRepository
{
    Task<IReadOnlyCollection<Cliente>> ObtenerTodosAsync();
    Task<Cliente?> ObtenerPorIdAsync(int id);
    Task<Cliente?> ObtenerPorIdentificacionAsync(string identificacion);
    Task<bool> ExisteIdentificacionAsync(string identificacion);
    Task AgregarAsync(Cliente cliente);
    Task ActualizarAsync(Cliente cliente);
}
