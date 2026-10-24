using GymRed.Application.DTOs;

namespace GymRed.Application.Interfaces.Services;

public interface IPlanService
{
    Task<IReadOnlyCollection<PlanDto>> ObtenerTodosAsync();
    Task<PlanDto?> ObtenerPorIdAsync(int id);
    Task<int> CrearAsync(CrearPlanDto dto);
    Task ActualizarAsync(int id, CrearPlanDto dto);
    Task CambiarEstadoAsync(int id, bool activo);
}
