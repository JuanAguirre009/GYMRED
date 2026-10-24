using GymRed.Application.DTOs;
using GymRed.Domain.Entities;

namespace GymRed.Application.Mappings;

public static class MembresiaMapping
{
    public static MembresiaDto ToDto(this Membresia membresia)
    {
        return new MembresiaDto(
            membresia.Id,
            membresia.ClienteId,
            membresia.Cliente?.NombreCompleto ?? "Cliente no encontrado",
            membresia.PlanId,
            membresia.Plan?.Nombre ?? "Plan no encontrado",
            membresia.FechaInicio,
            membresia.FechaFin,
            membresia.Estado.ToString(),
            membresia.PrecioPagado.Monto);
    }
}
