namespace GymRed.Application.DTOs;

public record class MembresiaDto(
    int Id,
    int ClienteId,
    string NombreCliente,
    int PlanId,
    string NombrePlan,
    DateTime FechaInicio,
    DateTime FechaFin,
    string Estado,
    decimal PrecioPagado);
