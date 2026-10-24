using GymRed.Application.DTOs.Reportes;

namespace GymRed.Application.Interfaces.Services;

public interface IReporteService
{
    Task<ReporteIngresosDto> GenerarReporteIngresosAsync(DateTime inicio, DateTime fin);
    Task<ReporteMembresiasDto> GenerarReporteMembresiasAsync(DateTime fechaCorte);
    Task<ReporteAsistenciaDto> GenerarReporteAsistenciaAsync(DateTime inicio, DateTime fin);
}
