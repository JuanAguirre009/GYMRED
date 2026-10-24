namespace GymRed.Application.DTOs;

public record class AsistenciaDto(
    int Id,
    int SesionClaseId,
    string NombreClase,
    DateTime FechaHoraSesion,
    int ClienteId,
    string NombreCliente,
    DateTime FechaHoraRegistro,
    bool Asistio);
