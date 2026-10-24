using GymRed.Application.Interfaces.Repositories;
using GymRed.Domain.Entities;
using GymRed.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymRed.Infrastructure.Repositories;

public class PagoRepository : IPagoRepository
{
    private readonly GymRedDbContext _context;

    public PagoRepository(GymRedDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IReadOnlyCollection<Pago>> ObtenerTodosAsync()
    {
        List<Pago> pagos = await _context.Pagos
            .AsNoTracking()
            .Include(p => p.Membresia)
            .ToListAsync();

        return pagos.AsReadOnly();
    }

    public async Task<IReadOnlyCollection<Pago>> ObtenerPorMembresiaAsync(int membresiaId)
    {
        List<Pago> pagos = await _context.Pagos
            .AsNoTracking()
            .Where(p => p.MembresiaId == membresiaId)
            .ToListAsync();

        return pagos.AsReadOnly();
    }

    public async Task<IReadOnlyCollection<Pago>> ObtenerPorRangoFechasAsync(DateTime inicio, DateTime fin)
    {
        List<Pago> pagos = await _context.Pagos
            .AsNoTracking()
            .Where(p => p.FechaPago.Date >= inicio.Date && p.FechaPago.Date <= fin.Date)
            .ToListAsync();

        return pagos.AsReadOnly();
    }

    public async Task<Pago?> ObtenerPorIdAsync(int id)
    {
        return await _context.Pagos
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AgregarAsync(Pago pago)
    {
        await _context.Pagos.AddAsync(pago);
    }
}
