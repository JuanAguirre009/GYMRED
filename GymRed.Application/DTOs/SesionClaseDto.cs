namespace GymRed.Application.DTOs;

public record class SesionClaseDto(
    int Id,
    int ClaseId,
    string NombreClase,
    DateTime FechaHoraInicio,
    DateTime FechaHoraFin,
    int CapacidadMaxima,
    int ReservasActivas,
    int CuposDisponibles,
    string Estado);
