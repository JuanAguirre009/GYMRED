using GymRed.Domain.Entities;

namespace GymRed.Application.Interfaces.Repositories;

public interface IPagoRepository
{
    Task<IReadOnlyCollection<Pago>> ObtenerTodosAsync();
    Task<IReadOnlyCollection<Pago>> ObtenerPorMembresiaAsync(int membresiaId);
    Task<IReadOnlyCollection<Pago>> ObtenerPorRangoFechasAsync(DateTime inicio, DateTime fin);
    Task<Pago?> ObtenerPorIdAsync(int id);
    Task AgregarAsync(Pago pago);
}
