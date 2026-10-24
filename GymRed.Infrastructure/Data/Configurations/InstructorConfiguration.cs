using GymRed.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymRed.Infrastructure.Data.Configurations;

public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.ToTable("Instructores");
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Nombres).HasMaxLength(100).IsRequired();
        builder.Property(i => i.Apellidos).HasMaxLength(100).IsRequired();
        builder.Property(i => i.Email).HasMaxLength(100).IsRequired();
        builder.Property(i => i.Telefono).HasMaxLength(20).IsRequired();
        builder.Property(i => i.Especialidad).HasMaxLength(100).IsRequired();
    }
}
