namespace ProyectoApi.Dto
{
    public class SalidaDtoOuput
    {
        //Dto de salida Pruebas.
        public int IdSalida { get; set; }
        public int IdDestino { get; set; }
        public DateTime? Fhsalida { get; set; }
        public int IdUsuarioRegistro { get; set; }
        public string? Direccion { get; set; }
        public string? Descripcion { get; set; }
        public string? Unidad { get; set; }
        public string? NombreUsuarioRegistro { get; set; }
    }
}
