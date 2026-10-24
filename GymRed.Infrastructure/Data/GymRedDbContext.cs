using GymRed.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymRed.Infrastructure.Data;

public class GymRedDbContext : DbContext
{
    public GymRedDbContext(DbContextOptions<GymRedDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cliente> Clientes => Set<Cliente>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // TODO: configurar entidades y relaciones en la rama capa-infraestructura
    }
}
