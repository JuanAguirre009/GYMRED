namespace GymRed.Application.DTOs;

public record class ClienteDto(
    int Id,
    string Nombres,
    string Apellidos,
    string NumeroIdentificacion,
    string Email,
    string Telefono,
    DateTime FechaNacimiento,
    DateTime FechaRegistro,
    string Estado,
    int SedeId,
    string NombreSede);
