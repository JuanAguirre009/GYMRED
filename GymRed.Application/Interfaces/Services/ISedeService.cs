using GymRed.Application.DTOs;

namespace GymRed.Application.Interfaces.Services;

public interface ISedeService
{
    Task<IReadOnlyCollection<SedeDto>> ObtenerTodasAsync();
    Task<SedeDto?> ObtenerPorIdAsync(int id);
    Task<int> CrearAsync(CrearSedeDto dto);
    Task ActualizarAsync(int id, CrearSedeDto dto);
    Task CambiarEstadoAsync(int id, bool activa);
}
