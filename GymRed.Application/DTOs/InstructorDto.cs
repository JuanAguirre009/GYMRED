namespace GymRed.Application.DTOs;

public record class InstructorDto(
    int Id,
    string Nombres,
    string Apellidos,
    string Email,
    string Telefono,
    string Especialidad,
    bool Activo);
