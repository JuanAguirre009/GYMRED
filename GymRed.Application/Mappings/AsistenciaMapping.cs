using GymRed.Application.DTOs;
using GymRed.Domain.Entities;

namespace GymRed.Application.Mappings;

public static class AsistenciaMapping
{
    public static AsistenciaDto ToDto(this Asistencia asistencia)
    {
        return new AsistenciaDto(
            asistencia.Id,
            asistencia.SesionClaseId,
            asistencia.SesionClase?.Clase?.Nombre ?? "Clase no encontrada",
            asistencia.SesionClase?.FechaHoraInicio ?? DateTime.MinValue,
            asistencia.ClienteId,
            asistencia.Cliente?.NombreCompleto ?? "Cliente no encontrado",
            asistencia.FechaHoraRegistro,
            asistencia.Asistio);
    }
}
