using GymRed.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymRed.Infrastructure.Data.Configurations;

public class AsistenciaConfiguration : IEntityTypeConfiguration<Asistencia>
{
    public void Configure(EntityTypeBuilder<Asistencia> builder)
    {
        builder.ToTable("Asistencias");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.FechaHoraRegistro).HasColumnType("datetime2");

        builder.HasOne(a => a.Cliente)
            .WithMany(c => c.Asistencias)
            .HasForeignKey(a => a.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.SesionClase)
            .WithMany()
            .HasForeignKey(a => a.SesionClaseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(a => new { a.SesionClaseId, a.ClienteId }).IsUnique();
    }
}
