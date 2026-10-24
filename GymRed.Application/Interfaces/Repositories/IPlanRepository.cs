using GymRed.Domain.Entities;

namespace GymRed.Application.Interfaces.Repositories;

public interface IPlanRepository
{
    Task<IReadOnlyCollection<Plan>> ObtenerTodosAsync();
    Task<Plan?> ObtenerPorIdAsync(int id);
    Task AgregarAsync(Plan plan);
    Task ActualizarAsync(Plan plan);
}
