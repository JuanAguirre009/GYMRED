using GymRed.Application.Interfaces.Repositories;
using GymRed.Domain.Entities;
using GymRed.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymRed.Infrastructure.Repositories;

public class PlanRepository : IPlanRepository
{
    private readonly GymRedDbContext _context;

    public PlanRepository(GymRedDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IReadOnlyCollection<Plan>> ObtenerTodosAsync()
    {
        List<Plan> planes = await _context.Planes
            .AsNoTracking()
            .ToListAsync();

        return planes.AsReadOnly();
    }

    public async Task<Plan?> ObtenerPorIdAsync(int id)
    {
        return await _context.Planes
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AgregarAsync(Plan plan)
    {
        await _context.Planes.AddAsync(plan);
    }

    public Task ActualizarAsync(Plan plan)
    {
        _context.Planes.Update(plan);
        return Task.CompletedTask;
    }
}
