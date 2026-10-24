using GymRed.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymRed.Infrastructure.Data.Configurations;

public class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.ToTable("Planes");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nombre).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Descripcion).HasMaxLength(500).IsRequired();
        builder.Property(p => p.DuracionDias).IsRequired();
        builder.Property(p => p.Beneficios).HasMaxLength(500).IsRequired();

        builder.OwnsOne(p => p.Precio, dinero =>
        {
            dinero.Property(d => d.Monto).HasColumnName("Precio").HasColumnType("decimal(18,2)").IsRequired();
            dinero.Property(d => d.Moneda).HasColumnName("Moneda").HasMaxLength(10).IsRequired();
        });
    }
}
