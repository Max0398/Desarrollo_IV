using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoApi.Models;

namespace ProyectoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalidaController : ControllerBase
    {
        private readonly UnidadesTransporteContext db = new UnidadesTransporteContext();
        [HttpGet]
        public IActionResult GetAll()
        {
            var salidas = (from salida in db.Salida
                           select new SalidaDtoOuput
                           {
                               IdSalida = salida.IdSalida,
                               Descripcion = salida.IdDestinoNavigation.Descripcion,
                               Direccion = salida.IdDestinoNavigation.Direccion,
                               Fhsalida = salida.Fhsalida,
                               Unidad = salida.IdDestinoNavigation.IdUnidadNavigation.Descripcion,
                               IdDestino=salida.IdDestino,
                               IdUsuarioRegistro=salida.IdUsuarioRegistro,
                               NombreUsuarioRegistro=salida.IdUsuarioRegistroNavigation.Nombre
                           }).ToList();
            return Ok(salidas);
        }

        [HttpGet ("{id}")]
        public IActionResult GetById(int id)
        {
            Salida? salida = (from salidadb in db.Salida
                              where salidadb.IdSalida.Equals(id)
                              select salidadb).FirstOrDefault();
            if(salida is null)
            {
                return NotFound();
            }
            return Ok(salida);
        }

    }
}
