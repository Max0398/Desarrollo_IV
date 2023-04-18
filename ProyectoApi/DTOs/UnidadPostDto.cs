namespace ProyectoApi.DTOs
{
    public class UnidadPostDto
    {
        public string Nplaca { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string Descripcion { get; set; }
        public int IdUsuarioRegistro { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
