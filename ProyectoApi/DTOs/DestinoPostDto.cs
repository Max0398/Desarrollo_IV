namespace ProyectoApi.DTOs
{
    public class DestinoPostDto
    {
        public string? Descripcion { get; set; }
        public string? Direccion { get; set; }
        public int IdDepartamento { get; set; }
        public int IdMunicipio { get; set; }
        public int IdUnidad { get; set; }
        public int IdConductor { get; set; }
        public int IdTipoViaje { get; set; }
        public int IdUsuarioRegistro { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
