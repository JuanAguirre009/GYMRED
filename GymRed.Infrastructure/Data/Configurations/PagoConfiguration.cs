using GymRed.Domain.Entities;
using GymRed.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymRed.Infrastructure.Data.Configurations;

public class PagoConfiguration : IEntityTypeConfiguration<Pago>
{
    public void Configure(EntityTypeBuilder<Pago> builder)
    {
        builder.ToTable("Pagos");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.FechaPago).HasColumnType("datetime2");
        builder.Property(p => p.NumeroTransaccion).HasMaxLength(50);
        builder.Property(p => p.MotivoAnulacion).HasMaxLength(500);

        builder.Property(p => p.MetodoPago)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(p => p.Estado)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.OwnsOne(p => p.Monto, dinero =>
        {
            dinero.Property(d => d.Monto).HasColumnName("Monto").HasColumnType("decimal(18,2)").IsRequired();
            dinero.Property(d => d.Moneda).HasColumnName("Moneda").HasMaxLength(10).IsRequired();
        });

        builder.HasOne(p => p.Membresia)
            .WithMany(m => m.Pagos)
            .HasForeignKey(p => p.MembresiaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
