using GymRed.Domain.Entities;

namespace GymRed.Application.Interfaces.Repositories;

public interface ISedeRepository
{
    Task<IReadOnlyCollection<Sede>> ObtenerTodasAsync();
    Task<Sede?> ObtenerPorIdAsync(int id);
    Task AgregarAsync(Sede sede);
    Task ActualizarAsync(Sede sede);
}
