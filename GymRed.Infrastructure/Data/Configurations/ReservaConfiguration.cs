using GymRed.Domain.Entities;
using GymRed.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymRed.Infrastructure.Data.Configurations;

public class ReservaConfiguration : IEntityTypeConfiguration<Reserva>
{
    public void Configure(EntityTypeBuilder<Reserva> builder)
    {
        builder.ToTable("Reservas");
        builder.HasKey(r => r.Id);

        builder.Property(r => r.FechaReserva).HasColumnType("datetime2");

        builder.Property(r => r.Estado)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasOne(r => r.Cliente)
            .WithMany(c => c.Reservas)
            .HasForeignKey(r => r.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.SesionClase)
            .WithMany()
            .HasForeignKey(r => r.SesionClaseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(r => new { r.SesionClaseId, r.ClienteId })
            .IsUnique()
            .HasFilter("[Estado] = 'Activa'");
    }
}
