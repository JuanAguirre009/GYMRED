namespace GymRed.Application.DTOs;

public record class SedeDto(
    int Id,
    string Nombre,
    string Direccion,
    string Telefono,
    string Email,
    DateTime FechaApertura,
    bool Activa);
