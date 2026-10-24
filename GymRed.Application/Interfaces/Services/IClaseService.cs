using GymRed.Application.DTOs;

namespace GymRed.Application.Interfaces.Services;

public interface IClaseService
{
    Task<IReadOnlyCollection<ClaseDto>> ObtenerTodasAsync();
    Task<ClaseDto?> ObtenerPorIdAsync(int id);
    Task<int> CrearAsync(CrearClaseDto dto);
    Task ActualizarAsync(int id, CrearClaseDto dto);
    Task CambiarEstadoAsync(int id, bool activo);
    Task<SesionClaseDto> ProgramarSesionAsync(ProgramarSesionDto dto);
    Task<IReadOnlyCollection<SesionClaseDto>> ObtenerSesionesPorClaseAsync(int claseId);
}
