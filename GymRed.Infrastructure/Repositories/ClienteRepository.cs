using GymRed.Application.Interfaces.Repositories;
using GymRed.Domain.Entities;
using GymRed.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymRed.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly GymRedDbContext _context;

    public ClienteRepository(GymRedDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IReadOnlyCollection<Cliente>> ObtenerTodosAsync()
    {
        List<Cliente> clientes = await _context.Clientes
            .AsNoTracking()
            .Include(c => c.Sede)
            .ToListAsync();

        return clientes.AsReadOnly();
    }

    public async Task<Cliente?> ObtenerPorIdAsync(int id)
    {
        return await _context.Clientes
            .Include(c => c.Sede)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Cliente?> ObtenerPorIdentificacionAsync(string identificacion)
    {
        return await _context.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.NumeroIdentificacion == identificacion);
    }

    public async Task<bool> ExisteIdentificacionAsync(string identificacion)
    {
        return await _context.Clientes
            .AsNoTracking()
            .AnyAsync(c => c.NumeroIdentificacion == identificacion);
    }

    public async Task AgregarAsync(Cliente cliente)
    {
        await _context.Clientes.AddAsync(cliente);
    }

    public Task ActualizarAsync(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
        return Task.CompletedTask;
    }
}
