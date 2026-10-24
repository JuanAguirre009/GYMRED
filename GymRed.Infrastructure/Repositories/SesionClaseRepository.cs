using GymRed.Application.Interfaces.Repositories;
using GymRed.Domain.Entities;
using GymRed.Domain.Enums;
using GymRed.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymRed.Infrastructure.Repositories;

public class SesionClaseRepository : ISesionClaseRepository
{
    private readonly GymRedDbContext _context;

    public SesionClaseRepository(GymRedDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<SesionClase?> ObtenerPorIdAsync(int id)
    {
        return await _context.SesionesClase
            .Include(s => s.Clase)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IReadOnlyCollection<SesionClase>> ObtenerPorClaseAsync(int claseId)
    {
        List<SesionClase> sesiones = await _context.SesionesClase
            .AsNoTracking()
            .Include(s => s.Clase)
            .Where(s => s.ClaseId == claseId)
            .ToListAsync();

        return sesiones.AsReadOnly();
    }

    public async Task<IReadOnlyCollection<SesionClase>> ObtenerProgramadasAsync(DateTime? fecha = null)
    {
        IQueryable<SesionClase> query = _context.SesionesClase
            .AsNoTracking()
            .Include(s => s.Clase)
            .Where(s => s.Estado == EstadoSesionClase.Programada);

        if (fecha.HasValue)
        {
            query = query.Where(s => s.FechaHoraInicio.Date == fecha.Value.Date);
        }

        List<SesionClase> sesiones = await query.ToListAsync();
        return sesiones.AsReadOnly();
    }

    public async Task<IReadOnlyCollection<SesionClase>> ObtenerPorRangoFechasAsync(DateTime inicio, DateTime fin)
    {
        List<SesionClase> sesiones = await _context.SesionesClase
            .AsNoTracking()
            .Where(s => s.FechaHoraInicio.Date >= inicio.Date && s.FechaHoraInicio.Date <= fin.Date)
            .ToListAsync();

        return sesiones.AsReadOnly();
    }

    public async Task<int> ContarReservasActivasAsync(int sesionClaseId)
    {
        return await _context.Reservas
            .AsNoTracking()
            .CountAsync(r => r.SesionClaseId == sesionClaseId && r.Estado == Domain.Enums.EstadoReserva.Activa);
    }

    public async Task AgregarAsync(SesionClase sesion)
    {
        await _context.SesionesClase.AddAsync(sesion);
    }

    public Task ActualizarAsync(SesionClase sesion)
    {
        _context.SesionesClase.Update(sesion);
        return Task.CompletedTask;
    }
}
