using GymRed.Domain.Entities;

namespace GymRed.Application.Interfaces.Repositories;

public interface IMembresiaRepository
{
    Task<IReadOnlyCollection<Membresia>> ObtenerTodasAsync();
    Task<Membresia?> ObtenerPorIdAsync(int id);
    Task<IReadOnlyCollection<Membresia>> ObtenerPorClienteAsync(int clienteId);
    Task<IReadOnlyCollection<Membresia>> ObtenerActivasAsync();
    Task<IReadOnlyCollection<Membresia>> ObtenerVencidasAsync();
    Task AgregarAsync(Membresia membresia);
    Task ActualizarAsync(Membresia membresia);
}
