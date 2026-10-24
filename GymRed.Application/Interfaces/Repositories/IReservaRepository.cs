using GymRed.Domain.Entities;

namespace GymRed.Application.Interfaces.Repositories;

public interface IReservaRepository
{
    Task<Reserva?> ObtenerPorIdAsync(int id);
    Task<IReadOnlyCollection<Reserva>> ObtenerPorClienteAsync(int clienteId);
    Task<IReadOnlyCollection<Reserva>> ObtenerPorSesionAsync(int sesionClaseId);
    Task<bool> ExisteReservaActivaAsync(int sesionClaseId, int clienteId);
    Task AgregarAsync(Reserva reserva);
    Task ActualizarAsync(Reserva reserva);
}
