namespace GymRed.Application.DTOs.Reportes;

public record class ReporteIngresosDto(
    DateTime FechaInicio,
    DateTime FechaFin,
    decimal TotalIngresos,
    int CantidadPagos,
    IReadOnlyCollection<PagoDto> DetallePagos);
