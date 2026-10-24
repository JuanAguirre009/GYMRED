using GymRed.Application.Interfaces.Repositories;
using GymRed.Domain.Entities;
using GymRed.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymRed.Infrastructure.Repositories;

public class ClaseRepository : IClaseRepository
{
    private readonly GymRedDbContext _context;

    public ClaseRepository(GymRedDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IReadOnlyCollection<Clase>> ObtenerTodosAsync()
    {
        List<Clase> clases = await _context.Clases
            .AsNoTracking()
            .Include(c => c.Instructor)
            .Include(c => c.Sede)
            .ToListAsync();

        return clases.AsReadOnly();
    }

    public async Task<Clase?> ObtenerPorIdAsync(int id)
    {
        return await _context.Clases
            .Include(c => c.Instructor)
            .Include(c => c.Sede)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AgregarAsync(Clase clase)
    {
        await _context.Clases.AddAsync(clase);
    }

    public Task ActualizarAsync(Clase clase)
    {
        _context.Clases.Update(clase);
        return Task.CompletedTask;
    }
}
