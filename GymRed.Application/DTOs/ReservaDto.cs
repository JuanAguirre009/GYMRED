namespace GymRed.Application.DTOs;

public record class ReservaDto(
    int Id,
    int SesionClaseId,
    string NombreClase,
    DateTime FechaHoraSesion,
    int ClienteId,
    string NombreCliente,
    DateTime FechaReserva,
    string Estado);
