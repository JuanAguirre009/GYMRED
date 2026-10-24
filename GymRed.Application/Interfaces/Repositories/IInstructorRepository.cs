using GymRed.Domain.Entities;

namespace GymRed.Application.Interfaces.Repositories;

public interface IInstructorRepository
{
    Task<IReadOnlyCollection<Instructor>> ObtenerTodosAsync();
    Task<Instructor?> ObtenerPorIdAsync(int id);
    Task AgregarAsync(Instructor instructor);
    Task ActualizarAsync(Instructor instructor);
}
