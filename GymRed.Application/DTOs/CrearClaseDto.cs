namespace GymRed.Application.DTOs;

public class CrearClaseDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public int InstructorId { get; set; }
    public int SedeId { get; set; }
    public int CapacidadMaxima { get; set; }
}
