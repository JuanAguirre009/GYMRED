using GymRed.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymRed.Infrastructure.Data.Configurations;

public class MembresiaConfiguration : IEntityTypeConfiguration<Membresia>
{
    public void Configure(EntityTypeBuilder<Membresia> builder)
    {
        builder.ToTable("Membresias");
        builder.HasKey(m => m.Id);

        builder.Property(m => m.FechaInicio).HasColumnType("date");
        builder.Property(m => m.FechaFin).HasColumnType("date");

        builder.OwnsOne(m => m.PrecioPagado, dinero =>
        {
            dinero.Property(d => d.Monto).HasColumnName("PrecioPagado").HasColumnType("decimal(18,2)").IsRequired();
            dinero.Property(d => d.Moneda).HasColumnName("MonedaPagada").HasMaxLength(10).IsRequired();
        });

        builder.HasOne(m => m.Cliente)
            .WithMany(c => c.Membresias)
            .HasForeignKey(m => m.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.Plan)
            .WithMany()
            .HasForeignKey(m => m.PlanId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
