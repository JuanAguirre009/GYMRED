using GymRed.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GymRed.Infrastructure.Data;

public class GymRedDbContext : DbContext
{
    public DbSet<Sede> Sedes => Set<Sede>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Instructor> Instructores => Set<Instructor>();
    public DbSet<Plan> Planes => Set<Plan>();
    public DbSet<Membresia> Membresias => Set<Membresia>();
    public DbSet<Pago> Pagos => Set<Pago>();
    public DbSet<Clase> Clases => Set<Clase>();
    public DbSet<SesionClase> SesionesClase => Set<SesionClase>();
    public DbSet<Reserva> Reservas => Set<Reserva>();
    public DbSet<Asistencia> Asistencias => Set<Asistencia>();

    public GymRedDbContext(DbContextOptions<GymRedDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
