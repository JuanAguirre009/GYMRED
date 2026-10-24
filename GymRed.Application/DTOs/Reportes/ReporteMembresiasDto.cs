namespace GymRed.Application.DTOs.Reportes;

public record class ReporteMembresiasDto(
    DateTime FechaCorte,
    int TotalActivas,
    int TotalVencidas,
    int TotalCanceladas,
    IReadOnlyCollection<MembresiaDto> DetalleMembresias);
