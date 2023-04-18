namespace ProyectoApi.DTOs
{
    public class ConductorPostDto
    {
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Nidentificacion { get; set; }
        public string? Ntelefono { get; set; }
        public string? Correo { get; set; }
        public string? Licencia { get; set; }
        public int IdUsuarioRegistro { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
