using GymRed.Application.DTOs;
using GymRed.Application.Exceptions;
using GymRed.Application.Interfaces.Repositories;
using GymRed.Application.Interfaces.Services;
using GymRed.Application.Mappings;
using GymRed.Domain.Entities;
using GymRed.Domain.ValueObjects;

namespace GymRed.Application.Services;

public class PlanService : IPlanService
{
    private readonly IPlanRepository _planRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PlanService(IPlanRepository planRepository, IUnitOfWork unitOfWork)
    {
        _planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<IReadOnlyCollection<PlanDto>> ObtenerTodosAsync()
    {
        IReadOnlyCollection<Plan> planes = await _planRepository.ObtenerTodosAsync();
        return planes.Select(p => p.ToDto()).ToList().AsReadOnly();
    }

    public async Task<PlanDto?> ObtenerPorIdAsync(int id)
    {
        Plan? plan = await _planRepository.ObtenerPorIdAsync(id);
        return plan?.ToDto();
    }

    public async Task<int> CrearAsync(CrearPlanDto dto)
    {
        if (dto is null)
            throw new AplicacionException("Los datos del plan son obligatorios.");

        Plan plan = new(
            dto.Nombre,
            dto.Descripcion,
            dto.DuracionDias,
            new Dinero(dto.Precio),
            dto.Beneficios);

        await _planRepository.AgregarAsync(plan);
        await _unitOfWork.SaveChangesAsync();

        return plan.Id;
    }

    public async Task ActualizarAsync(int id, CrearPlanDto dto)
    {
        if (dto is null)
            throw new AplicacionException("Los datos del plan son obligatorios.");

        Plan? plan = await _planRepository.ObtenerPorIdAsync(id)
            ?? throw new AplicacionException("Plan no encontrado.");

        plan.Actualizar(
            dto.Nombre,
            dto.Descripcion,
            dto.DuracionDias,
            new Dinero(dto.Precio),
            dto.Beneficios);

        await _planRepository.ActualizarAsync(plan);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task CambiarEstadoAsync(int id, bool activo)
    {
        Plan? plan = await _planRepository.ObtenerPorIdAsync(id)
            ?? throw new AplicacionException("Plan no encontrado.");

        plan.CambiarEstado(activo);
        await _planRepository.ActualizarAsync(plan);
        await _unitOfWork.SaveChangesAsync();
    }
}
