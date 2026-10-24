using GymRed.Application.Interfaces.Repositories;
using GymRed.Domain.Entities;
using GymRed.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymRed.Infrastructure.Repositories;

public class AsistenciaRepository : IAsistenciaRepository
{
    private readonly GymRedDbContext _context;

    public AsistenciaRepository(GymRedDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IReadOnlyCollection<Asistencia>> ObtenerPorSesionAsync(int sesionClaseId)
    {
        List<Asistencia> asistencias = await _context.Asistencias
            .AsNoTracking()
            .Include(a => a.Cliente)
            .Include(a => a.SesionClase)
            .ThenInclude(s => s != null ? s.Clase : null)
            .Where(a => a.SesionClaseId == sesionClaseId)
            .ToListAsync();

        return asistencias.AsReadOnly();
    }

    public async Task<IReadOnlyCollection<Asistencia>> ObtenerPorClienteAsync(int clienteId)
    {
        List<Asistencia> asistencias = await _context.Asistencias
            .AsNoTracking()
            .Include(a => a.SesionClase)
            .ThenInclude(s => s != null ? s.Clase : null)
            .Where(a => a.ClienteId == clienteId)
            .ToListAsync();

        return asistencias.AsReadOnly();
    }

    public async Task<IReadOnlyCollection<Asistencia>> ObtenerPorRangoFechasAsync(DateTime inicio, DateTime fin)
    {
        List<Asistencia> asistencias = await _context.Asistencias
            .AsNoTracking()
            .Where(a => a.FechaHoraRegistro.Date >= inicio.Date && a.FechaHoraRegistro.Date <= fin.Date)
            .ToListAsync();

        return asistencias.AsReadOnly();
    }

    public async Task<bool> ExisteAsistenciaAsync(int sesionClaseId, int clienteId)
    {
        return await _context.Asistencias
            .AsNoTracking()
            .AnyAsync(a => a.SesionClaseId == sesionClaseId && a.ClienteId == clienteId);
    }

    public async Task AgregarAsync(Asistencia asistencia)
    {
        await _context.Asistencias.AddAsync(asistencia);
    }
}
