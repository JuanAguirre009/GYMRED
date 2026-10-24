using GymRed.Application.Interfaces.Repositories;
using GymRed.Domain.Entities;
using GymRed.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymRed.Infrastructure.Repositories;

public class SedeRepository : ISedeRepository
{
    private readonly GymRedDbContext _context;

    public SedeRepository(GymRedDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IReadOnlyCollection<Sede>> ObtenerTodasAsync()
    {
        List<Sede> sedes = await _context.Sedes
            .AsNoTracking()
            .ToListAsync();

        return sedes.AsReadOnly();
    }

    public async Task<Sede?> ObtenerPorIdAsync(int id)
    {
        return await _context.Sedes
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task AgregarAsync(Sede sede)
    {
        await _context.Sedes.AddAsync(sede);
    }

    public Task ActualizarAsync(Sede sede)
    {
        _context.Sedes.Update(sede);
        return Task.CompletedTask;
    }
}
