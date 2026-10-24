namespace GymRed.Application.DTOs;

public class CrearSedeDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime FechaApertura { get; set; }
}
