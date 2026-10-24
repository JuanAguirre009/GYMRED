using GymRed.Application.DTOs;

namespace GymRed.Application.Interfaces.Services;

public interface IMembresiaService
{
    Task<MembresiaDto?> ObtenerPorIdAsync(int id);
    Task<IReadOnlyCollection<MembresiaDto>> ObtenerPorClienteAsync(int clienteId);
    Task<IReadOnlyCollection<MembresiaDto>> ObtenerActivasAsync();
    Task<IReadOnlyCollection<MembresiaDto>> ObtenerVencidasAsync();
    Task<MembresiaDto> AsignarAsync(AsignarMembresiaDto dto);
    Task CancelarAsync(int id);
    Task<bool> ClienteTieneMembresiaVigenteAsync(int clienteId);
}
