namespace ProyectoApi.DTOs
{
    public class UsuarioPostDto
    {
        public string? Nombre { get; set; }
        public string? Contrasenia { get; set; }
        public string? Correo { get; set; }
        public bool? Estado { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public int IdTipoUsuario { get; set; }
    }
}
