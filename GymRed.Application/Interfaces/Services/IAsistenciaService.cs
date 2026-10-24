using GymRed.Application.DTOs;

namespace GymRed.Application.Interfaces.Services;

public interface IAsistenciaService
{
    Task<IReadOnlyCollection<AsistenciaDto>> ObtenerPorClienteAsync(int clienteId);
    Task<IReadOnlyCollection<AsistenciaDto>> ObtenerPorSesionAsync(int sesionClaseId);
    Task<AsistenciaDto> RegistrarAsync(RegistrarAsistenciaDto dto);
}
