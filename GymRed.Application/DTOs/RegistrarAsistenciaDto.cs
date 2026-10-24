namespace GymRed.Application.DTOs;

public class RegistrarAsistenciaDto
{
    public int SesionClaseId { get; set; }
    public int ClienteId { get; set; }
    public bool Asistio { get; set; } = true;
}
