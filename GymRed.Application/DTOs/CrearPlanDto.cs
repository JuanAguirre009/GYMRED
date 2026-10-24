namespace GymRed.Application.DTOs;

public class CrearPlanDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public int DuracionDias { get; set; }
    public decimal Precio { get; set; }
    public string Beneficios { get; set; } = string.Empty;
}
