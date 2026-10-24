using GymRed.Domain.Entities;

namespace GymRed.Application.Interfaces.Repositories;

public interface ISesionClaseRepository
{
    Task<SesionClase?> ObtenerPorIdAsync(int id);
    Task<IReadOnlyCollection<SesionClase>> ObtenerPorClaseAsync(int claseId);
    Task<IReadOnlyCollection<SesionClase>> ObtenerProgramadasAsync(DateTime? fecha = null);
    Task<IReadOnlyCollection<SesionClase>> ObtenerPorRangoFechasAsync(DateTime inicio, DateTime fin);
    Task<int> ContarReservasActivasAsync(int sesionClaseId);
    Task AgregarAsync(SesionClase sesion);
    Task ActualizarAsync(SesionClase sesion);
}
