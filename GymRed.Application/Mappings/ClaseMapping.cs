using GymRed.Application.DTOs;
using GymRed.Domain.Entities;

namespace GymRed.Application.Mappings;

public static class ClaseMapping
{
    public static ClaseDto ToDto(this Clase clase)
    {
        return new ClaseDto(
            clase.Id,
            clase.Nombre,
            clase.Descripcion,
            clase.InstructorId,
            clase.Instructor?.NombreCompleto ?? "Instructor no asignado",
            clase.SedeId,
            clase.Sede?.Nombre ?? "Sede no asignada",
            clase.CapacidadMaxima,
            clase.Activa);
    }
}
