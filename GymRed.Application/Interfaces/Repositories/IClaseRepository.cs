using GymRed.Domain.Entities;

namespace GymRed.Application.Interfaces.Repositories;

public interface IClaseRepository
{
    Task<IReadOnlyCollection<Clase>> ObtenerTodosAsync();
    Task<Clase?> ObtenerPorIdAsync(int id);
    Task AgregarAsync(Clase clase);
    Task ActualizarAsync(Clase clase);
}
