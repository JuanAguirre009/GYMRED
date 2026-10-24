using GymRed.Application.DTOs;
using GymRed.Application.DTOs.Reportes;
using GymRed.Application.Interfaces.Repositories;
using GymRed.Application.Interfaces.Services;
using GymRed.Application.Mappings;
using GymRed.Domain.Entities;
using GymRed.Domain.Enums;

namespace GymRed.Application.Services;

public class ReporteService : IReporteService
{
    private readonly IPagoRepository _pagoRepository;
    private readonly IMembresiaRepository _membresiaRepository;
    private readonly IAsistenciaRepository _asistenciaRepository;
    private readonly ISesionClaseRepository _sesionRepository;

    public ReporteService(
        IPagoRepository pagoRepository,
        IMembresiaRepository membresiaRepository,
        IAsistenciaRepository asistenciaRepository,
        ISesionClaseRepository sesionRepository)
    {
        _pagoRepository = pagoRepository ?? throw new ArgumentNullException(nameof(pagoRepository));
        _membresiaRepository = membresiaRepository ?? throw new ArgumentNullException(nameof(membresiaRepository));
        _asistenciaRepository = asistenciaRepository ?? throw new ArgumentNullException(nameof(asistenciaRepository));
        _sesionRepository = sesionRepository ?? throw new ArgumentNullException(nameof(sesionRepository));
    }

    public async Task<ReporteIngresosDto> GenerarReporteIngresosAsync(DateTime inicio, DateTime fin)
    {
        IReadOnlyCollection<Pago> pagos = await _pagoRepository.ObtenerPorRangoFechasAsync(inicio, fin);
        IReadOnlyCollection<PagoDto> pagosDto = pagos
            .Where(p => p.Estado == EstadoPago.Registrado)
            .Select(p => p.ToDto())
            .ToList()
            .AsReadOnly();

        decimal total = pagosDto.Sum(p => p.Monto);

        return new ReporteIngresosDto(inicio, fin, total, pagosDto.Count, pagosDto);
    }

    public async Task<ReporteMembresiasDto> GenerarReporteMembresiasAsync(DateTime fechaCorte)
    {
        IReadOnlyCollection<Membresia> membresias = await _membresiaRepository.ObtenerTodasAsync();
        IReadOnlyCollection<MembresiaDto> detalle = membresias
            .Select(m => m.ToDto())
            .ToList()
            .AsReadOnly();

        int activas = detalle.Count(m => m.Estado == EstadoMembresia.Activa.ToString() && m.FechaFin >= fechaCorte.Date);
        int vencidas = detalle.Count(m => m.FechaFin < fechaCorte.Date || m.Estado == EstadoMembresia.Vencida.ToString());
        int canceladas = detalle.Count(m => m.Estado == EstadoMembresia.Cancelada.ToString());

        return new ReporteMembresiasDto(fechaCorte, activas, vencidas, canceladas, detalle);
    }

    public async Task<ReporteAsistenciaDto> GenerarReporteAsistenciaAsync(DateTime inicio, DateTime fin)
    {
        IReadOnlyCollection<Asistencia> asistencias = await _asistenciaRepository.ObtenerPorRangoFechasAsync(inicio, fin);
        IReadOnlyCollection<AsistenciaDto> detalle = asistencias
            .Select(a => a.ToDto())
            .ToList()
            .AsReadOnly();

        IReadOnlyCollection<SesionClase> sesiones = await _sesionRepository.ObtenerProgramadasAsync(null);
        int totalSesiones = sesiones.Count(s => s.FechaHoraInicio.Date >= inicio.Date && s.FechaHoraInicio.Date <= fin.Date);

        return new ReporteAsistenciaDto(inicio, fin, detalle.Count, totalSesiones, detalle);
    }
}
