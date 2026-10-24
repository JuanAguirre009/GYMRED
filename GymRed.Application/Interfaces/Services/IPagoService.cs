using GymRed.Application.DTOs;

namespace GymRed.Application.Interfaces.Services;

public interface IPagoService
{
    Task<IReadOnlyCollection<PagoDto>> ObtenerTodosAsync();
    Task<IReadOnlyCollection<PagoDto>> ObtenerPorMembresiaAsync(int membresiaId);
    Task<PagoDto> RegistrarAsync(RegistrarPagoDto dto);
    Task AnularAsync(int id, string motivo);
    Task<bool> ClienteEstaAlDiaAsync(int clienteId);
}
