using GymRed.Application.DTOs;

namespace GymRed.Application.Interfaces.Services;

public interface IReservaService
{
    Task<IReadOnlyCollection<ReservaDto>> ObtenerPorClienteAsync(int clienteId);
    Task<IReadOnlyCollection<ReservaDto>> ObtenerPorSesionAsync(int sesionClaseId);
    Task<ReservaDto> CrearAsync(CrearReservaDto dto);
    Task CancelarAsync(int id);
}
