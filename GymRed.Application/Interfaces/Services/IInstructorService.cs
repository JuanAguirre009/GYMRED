using GymRed.Application.DTOs;

namespace GymRed.Application.Interfaces.Services;

public interface IInstructorService
{
    Task<IReadOnlyCollection<InstructorDto>> ObtenerTodosAsync();
    Task<InstructorDto?> ObtenerPorIdAsync(int id);
    Task<int> CrearAsync(CrearInstructorDto dto);
    Task ActualizarAsync(int id, CrearInstructorDto dto);
    Task CambiarEstadoAsync(int id, bool activo);
}
