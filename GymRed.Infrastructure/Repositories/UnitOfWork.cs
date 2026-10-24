using GymRed.Application.Interfaces.Repositories;
using GymRed.Infrastructure.Data;

namespace GymRed.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly GymRedDbContext _context;

    public UnitOfWork(GymRedDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
