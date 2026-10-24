using GymRed.Application.Interfaces.Repositories;
using GymRed.Domain.Entities;
using GymRed.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymRed.Infrastructure.Repositories;

public class InstructorRepository : IInstructorRepository
{
    private readonly GymRedDbContext _context;

    public InstructorRepository(GymRedDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IReadOnlyCollection<Instructor>> ObtenerTodosAsync()
    {
        List<Instructor> instructores = await _context.Instructores
            .AsNoTracking()
            .ToListAsync();

        return instructores.AsReadOnly();
    }

    public async Task<Instructor?> ObtenerPorIdAsync(int id)
    {
        return await _context.Instructores
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task AgregarAsync(Instructor instructor)
    {
        await _context.Instructores.AddAsync(instructor);
    }

    public Task ActualizarAsync(Instructor instructor)
    {
        _context.Instructores.Update(instructor);
        return Task.CompletedTask;
    }
}
