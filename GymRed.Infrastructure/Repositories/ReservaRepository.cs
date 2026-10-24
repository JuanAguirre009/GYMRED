using GymRed.Application.Interfaces.Repositories;
using GymRed.Domain.Entities;
using GymRed.Domain.Enums;
using GymRed.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymRed.Infrastructure.Repositories;

public class ReservaRepository : IReservaRepository
{
    private readonly GymRedDbContext _context;

    public ReservaRepository(GymRedDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Reserva?> ObtenerPorIdAsync(int id)
    {
        return await _context.Reservas
            .Include(r => r.Cliente)
            .Include(r => r.SesionClase)
            .ThenInclude(s => s != null ? s.Clase : null)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IReadOnlyCollection<Reserva>> ObtenerPorClienteAsync(int clienteId)
    {
        List<Reserva> reservas = await _context.Reservas
            .AsNoTracking()
            .Include(r => r.SesionClase)
            .ThenInclude(s => s != null ? s.Clase : null)
            .Where(r => r.ClienteId == clienteId)
            .ToListAsync();

        return reservas.AsReadOnly();
    }

    public async Task<IReadOnlyCollection<Reserva>> ObtenerPorSesionAsync(int sesionClaseId)
    {
        List<Reserva> reservas = await _context.Reservas
            .AsNoTracking()
            .Include(r => r.Cliente)
            .Where(r => r.SesionClaseId == sesionClaseId)
            .ToListAsync();

        return reservas.AsReadOnly();
    }

    public async Task<bool> ExisteReservaActivaAsync(int sesionClaseId, int clienteId)
    {
        return await _context.Reservas
            .AsNoTracking()
            .AnyAsync(r => r.SesionClaseId == sesionClaseId
                && r.ClienteId == clienteId
                && r.Estado == EstadoReserva.Activa);
    }

    public async Task AgregarAsync(Reserva reserva)
    {
        await _context.Reservas.AddAsync(reserva);
    }

    public Task ActualizarAsync(Reserva reserva)
    {
        _context.Reservas.Update(reserva);
        return Task.CompletedTask;
    }
}
