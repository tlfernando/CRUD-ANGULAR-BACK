namespace Estudiantes.API.Models
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }
        public string? NombreCompleto { get; set; } = string.Empty;
        public string? Correo { get; set; } = string.Empty;
        public decimal Sueldo { get; set; }
        public string? FechaContrato { get; set; } = string.Empty;
    }
}
