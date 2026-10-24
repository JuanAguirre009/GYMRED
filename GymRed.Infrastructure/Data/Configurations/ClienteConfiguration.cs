using GymRed.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymRed.Infrastructure.Data.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nombres).HasMaxLength(100).IsRequired();
        builder.Property(c => c.Apellidos).HasMaxLength(100).IsRequired();
        builder.Property(c => c.NumeroIdentificacion).HasMaxLength(50).IsRequired();
        builder.Property(c => c.Email).HasMaxLength(100).IsRequired();
        builder.Property(c => c.Telefono).HasMaxLength(20).IsRequired();
        builder.Property(c => c.FechaNacimiento).HasColumnType("date");
        builder.Property(c => c.FechaRegistro).HasColumnType("datetime2");

        builder.HasIndex(c => c.NumeroIdentificacion).IsUnique();

        builder.HasOne(c => c.Sede)
            .WithMany()
            .HasForeignKey(c => c.SedeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
