using GymRed.Application.DTOs;
using GymRed.Domain.Entities;

namespace GymRed.Application.Mappings;

public static class ClienteMapping
{
    public static ClienteDto ToDto(this Cliente cliente)
    {
        return new ClienteDto(
            cliente.Id,
            cliente.Nombres,
            cliente.Apellidos,
            cliente.NumeroIdentificacion,
            cliente.Email,
            cliente.Telefono,
            cliente.FechaNacimiento,
            cliente.FechaRegistro,
            cliente.Estado.ToString(),
            cliente.SedeId,
            cliente.Sede?.Nombre ?? "Sede no asignada");
    }
}
