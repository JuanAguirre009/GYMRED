using GymRed.Application.DTOs;
using GymRed.Domain.Entities;

namespace GymRed.Application.Mappings;

public static class SesionClaseMapping
{
    public static SesionClaseDto ToDto(this SesionClase sesion, int reservasActivas)
    {
        return new SesionClaseDto(
            sesion.Id,
            sesion.ClaseId,
            sesion.Clase?.Nombre ?? "Clase no encontrada",
            sesion.FechaHoraInicio,
            sesion.FechaHoraFin,
            sesion.CapacidadMaxima,
            reservasActivas,
            sesion.CapacidadMaxima - reservasActivas,
            sesion.Estado.ToString());
    }
}
