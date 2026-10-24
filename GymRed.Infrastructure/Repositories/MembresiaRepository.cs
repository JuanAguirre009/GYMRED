using GymRed.Application.Interfaces.Repositories;
using GymRed.Domain.Entities;
using GymRed.Domain.Enums;
using GymRed.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymRed.Infrastructure.Repositories;

public class MembresiaRepository : IMembresiaRepository
{
    private readonly GymRedDbContext _context;

    public MembresiaRepository(GymRedDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IReadOnlyCollection<Membresia>> ObtenerTodasAsync()
    {
        List<Membresia> membresias = await _context.Membresias
            .AsNoTracking()
            .Include(m => m.Cliente)
            .Include(m => m.Plan)
            .ToListAsync();

        return membresias.AsReadOnly();
    }

    public async Task<Membresia?> ObtenerPorIdAsync(int id)
    {
        return await _context.Membresias
            .Include(m => m.Cliente)
            .Include(m => m.Plan)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<IReadOnlyCollection<Membresia>> ObtenerPorClienteAsync(int clienteId)
    {
        List<Membresia> membresias = await _context.Membresias
            .AsNoTracking()
            .Include(m => m.Plan)
            .Where(m => m.ClienteId == clienteId)
            .ToListAsync();

        return membresias.AsReadOnly();
    }

    public async Task<IReadOnlyCollection<Membresia>> ObtenerActivasAsync()
    {
        List<Membresia> membresias = await _context.Membresias
            .AsNoTracking()
            .Include(m => m.Cliente)
            .Include(m => m.Plan)
            .Where(m => m.Estado == EstadoMembresia.Activa)
            .ToListAsync();

        return membresias.AsReadOnly();
    }

    public async Task<IReadOnlyCollection<Membresia>> ObtenerVencidasAsync()
    {
        List<Membresia> membresias = await _context.Membresias
            .AsNoTracking()
            .Include(m => m.Cliente)
            .Include(m => m.Plan)
            .Where(m => m.Estado == EstadoMembresia.Vencida)
            .ToListAsync();

        return membresias.AsReadOnly();
    }

    public async Task AgregarAsync(Membresia membresia)
    {
        await _context.Membresias.AddAsync(membresia);
    }

    public Task ActualizarAsync(Membresia membresia)
    {
        _context.Membresias.Update(membresia);
        return Task.CompletedTask;
    }
}
