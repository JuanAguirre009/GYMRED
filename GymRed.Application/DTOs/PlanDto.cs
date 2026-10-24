namespace GymRed.Application.DTOs;

public record class PlanDto(
    int Id,
    string Nombre,
    string Descripcion,
    int DuracionDias,
    decimal Precio,
    string Beneficios,
    bool Activo);
