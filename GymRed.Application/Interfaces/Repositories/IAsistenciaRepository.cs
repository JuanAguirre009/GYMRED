using GymRed.Domain.Entities;

namespace GymRed.Application.Interfaces.Repositories;

public interface IAsistenciaRepository
{
    Task<IReadOnlyCollection<Asistencia>> ObtenerPorSesionAsync(int sesionClaseId);
    Task<IReadOnlyCollection<Asistencia>> ObtenerPorClienteAsync(int clienteId);
    Task<IReadOnlyCollection<Asistencia>> ObtenerPorRangoFechasAsync(DateTime inicio, DateTime fin);
    Task<bool> ExisteAsistenciaAsync(int sesionClaseId, int clienteId);
    Task AgregarAsync(Asistencia asistencia);
}
