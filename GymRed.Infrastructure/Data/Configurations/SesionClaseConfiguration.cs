using GymRed.Domain.Entities;
using GymRed.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymRed.Infrastructure.Data.Configurations;

public class SesionClaseConfiguration : IEntityTypeConfiguration<SesionClase>
{
    public void Configure(EntityTypeBuilder<SesionClase> builder)
    {
        builder.ToTable("SesionesClase");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Estado)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasOne(s => s.Clase)
            .WithMany(c => c.Sesiones)
            .HasForeignKey(s => s.ClaseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
