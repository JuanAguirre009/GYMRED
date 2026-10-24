using GymRed.Application.DTOs;
using GymRed.Domain.Entities;

namespace GymRed.Application.Mappings;

public static class ReservaMapping
{
    public static ReservaDto ToDto(this Reserva reserva)
    {
        return new ReservaDto(
            reserva.Id,
            reserva.SesionClaseId,
            reserva.SesionClase?.Clase?.Nombre ?? "Clase no encontrada",
            reserva.SesionClase?.FechaHoraInicio ?? DateTime.MinValue,
            reserva.ClienteId,
            reserva.Cliente?.NombreCompleto ?? "Cliente no encontrado",
            reserva.FechaReserva,
            reserva.Estado.ToString());
    }
}
