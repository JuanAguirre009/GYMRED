namespace GymRed.Application.DTOs;

public record class ClaseDto(
    int Id,
    string Nombre,
    string Descripcion,
    int InstructorId,
    string NombreInstructor,
    int SedeId,
    string NombreSede,
    int CapacidadMaxima,
    bool Activa);
