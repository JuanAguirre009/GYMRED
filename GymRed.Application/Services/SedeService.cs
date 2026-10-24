using GymRed.Application.DTOs;
using GymRed.Application.Exceptions;
using GymRed.Application.Interfaces.Repositories;
using GymRed.Application.Interfaces.Services;
using GymRed.Application.Mappings;
using GymRed.Domain.Entities;

namespace GymRed.Application.Services;

public class SedeService : ISedeService
{
    private readonly ISedeRepository _sedeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SedeService(ISedeRepository sedeRepository, IUnitOfWork unitOfWork)
    {
        _sedeRepository = sedeRepository ?? throw new ArgumentNullException(nameof(sedeRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<IReadOnlyCollection<SedeDto>> ObtenerTodasAsync()
    {
        IReadOnlyCollection<Sede> sedes = await _sedeRepository.ObtenerTodasAsync();
        return sedes.Select(s => s.ToDto()).ToList().AsReadOnly();
    }

    public async Task<SedeDto?> ObtenerPorIdAsync(int id)
    {
        Sede? sede = await _sedeRepository.ObtenerPorIdAsync(id);
        return sede?.ToDto();
    }

    public async Task<int> CrearAsync(CrearSedeDto dto)
    {
        if (dto is null)
            throw new AplicacionException("Los datos de la sede son obligatorios.");

        Sede sede = new(dto.Nombre, dto.Direccion, dto.Telefono, dto.Email, dto.FechaApertura);
        await _sedeRepository.AgregarAsync(sede);
        await _unitOfWork.SaveChangesAsync();

        return sede.Id;
    }

    public async Task ActualizarAsync(int id, CrearSedeDto dto)
    {
        if (dto is null)
            throw new AplicacionException("Los datos de la sede son obligatorios.");

        Sede? sede = await _sedeRepository.ObtenerPorIdAsync(id)
            ?? throw new AplicacionException("Sede no encontrada.");

        sede.Actualizar(dto.Nombre, dto.Direccion, dto.Telefono, dto.Email);
        await _sedeRepository.ActualizarAsync(sede);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task CambiarEstadoAsync(int id, bool activa)
    {
        Sede? sede = await _sedeRepository.ObtenerPorIdAsync(id)
            ?? throw new AplicacionException("Sede no encontrada.");

        sede.CambiarEstado(activa);
        await _sedeRepository.ActualizarAsync(sede);
        await _unitOfWork.SaveChangesAsync();
    }
}
