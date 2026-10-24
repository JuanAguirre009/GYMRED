using GymRed.Application.DTOs;
using GymRed.Domain.Entities;

namespace GymRed.Application.Mappings;

public static class PlanMapping
{
    public static PlanDto ToDto(this Plan plan)
    {
        return new PlanDto(
            plan.Id,
            plan.Nombre,
            plan.Descripcion,
            plan.DuracionDias,
            plan.Precio.Monto,
            plan.Beneficios,
            plan.Activo);
    }
}
