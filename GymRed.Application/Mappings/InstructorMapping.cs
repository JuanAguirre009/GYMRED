using GymRed.Application.DTOs;
using GymRed.Domain.Entities;

namespace GymRed.Application.Mappings;

public static class InstructorMapping
{
    public static InstructorDto ToDto(this Instructor instructor)
    {
        return new InstructorDto(
            instructor.Id,
            instructor.Nombres,
            instructor.Apellidos,
            instructor.Email,
            instructor.Telefono,
            instructor.Especialidad,
            instructor.Activo);
    }
}
