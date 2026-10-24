namespace GymRed.Application.DTOs.Reportes;

public record class ReporteAsistenciaDto(
    DateTime FechaInicio,
    DateTime FechaFin,
    int TotalAsistencias,
    int TotalSesiones,
    IReadOnlyCollection<AsistenciaDto> DetalleAsistencias);
