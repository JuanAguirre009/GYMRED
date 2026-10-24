using GymRed.Application.DTOs;
using GymRed.Domain.Entities;

namespace GymRed.Application.Mappings;

public static class SedeMapping
{
    public static SedeDto ToDto(this Sede sede)
    {
        return new SedeDto(
            sede.Id,
            sede.Nombre,
            sede.Direccion,
            sede.Telefono,
            sede.Email,
            sede.FechaApertura,
            sede.Activa);
    }
}
