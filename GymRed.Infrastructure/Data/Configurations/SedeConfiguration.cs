using GymRed.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymRed.Infrastructure.Data.Configurations;

public class SedeConfiguration : IEntityTypeConfiguration<Sede>
{
    public void Configure(EntityTypeBuilder<Sede> builder)
    {
        builder.ToTable("Sedes");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Nombre).HasMaxLength(100).IsRequired();
        builder.Property(s => s.Direccion).HasMaxLength(200).IsRequired();
        builder.Property(s => s.Telefono).HasMaxLength(20).IsRequired();
        builder.Property(s => s.Email).HasMaxLength(100).IsRequired();
        builder.Property(s => s.FechaApertura).HasColumnType("date");
    }
}
