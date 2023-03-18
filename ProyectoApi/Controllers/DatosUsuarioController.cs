using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoApi.Models;

namespace ProyectoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatosUsuarioController : ControllerBase
    {
        private readonly UnidadesTransporteContext db = new UnidadesTransporteContext();

        [HttpGet]
        public IEnumerable<Object> GetAll()
        {
            var DatosUsuario = (from datos in db.DatosUsuarios
                                select datos).ToList();
            return DatosUsuario;
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            DatosUsuario? datos = (from Datos in db.DatosUsuarios
                                  where Datos.IdDatos.Equals(id)
                                  select Datos).FirstOrDefault();
            if(datos is null)
            {
                return BadRequest("Usuario No Encontrado");
            }
            return Ok(datos);   

        }

        [HttpPost]
        public IActionResult Post(DatosUsuario datosUsaurio)
        {
            if (datosUsaurio.Direccion == null || datosUsaurio.Nombres == null || datosUsaurio.Apellidos == null)
            {
                return BadRequest("Se Encontraron Campos Vacios");
            }
            db.DatosUsuarios.Add(datosUsaurio);
            db.SaveChanges();
            return Ok();    

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id,DatosUsuario datosUsuario)
        {
            DatosUsuario? datosUsuariodb = (from datos in db.DatosUsuarios
                                            where datos.IdDatos.Equals(id)
                                            select datos).FirstOrDefault();
            if(datosUsuariodb is null)
            {
                return BadRequest();
            }
            datosUsuariodb.Nombres=datosUsuario.Nombres;
            datosUsuariodb.Apellidos = datosUsuario.Apellidos;
            datosUsuariodb.Direccion = datosUsuario.Direccion;
            datosUsuario.Telefono = datosUsuario.Telefono;
            datosUsuariodb.Nidentificacion = datosUsuario.Nidentificacion;
            db.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DatosUsuario? datosEliminar = new DatosUsuario() { IdDatos = id };
            if (datosEliminar is null)
            {
                return BadRequest($"No se Encontro Datos con el ID:{id}");
            }
            db.DatosUsuarios.Remove(datosEliminar);
            db.SaveChanges();
            return Ok($"Se Elimino los Datos con el ID:{id}");
        }
    }

}
