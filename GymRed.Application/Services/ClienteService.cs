using GymRed.Application.Interfaces;
using GymRed.Domain.Entities;

namespace GymRed.Application.Services;

public class ClienteService : IClienteService
{
    // TODO: inyectar repositorio de infraestructura en la rama correspondiente

    public Task<IEnumerable<Cliente>> ObtenerTodosAsync()
    {
        throw new NotImplementedException("Pendiente de implementar en la rama capa-aplicacion.");
    }

    public Task<Cliente?> ObtenerPorIdAsync(int id)
    {
        throw new NotImplementedException("Pendiente de implementar en la rama capa-aplicacion.");
    }

    public Task<int> CrearAsync(Cliente cliente)
    {
        throw new NotImplementedException("Pendiente de implementar en la rama capa-aplicacion.");
    }

    public Task ActualizarAsync(Cliente cliente)
    {
        throw new NotImplementedException("Pendiente de implementar en la rama capa-aplicacion.");
    }

    public Task CambiarEstadoAsync(int id, bool activo)
    {
        throw new NotImplementedException("Pendiente de implementar en la rama capa-aplicacion.");
    }
}
