namespace ProyectoApi.DTOs
{
    public class DatosUsuarioPostDto
    {
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Nidentificacion { get; set; }
        public int IdUsuario { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
