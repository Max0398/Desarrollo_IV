using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoApi.Models;

namespace ProyectoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConductorController : ControllerBase
    {
        private readonly UnidadesTransporteContext db = new UnidadesTransporteContext();

        [HttpGet]
        public IEnumerable<Object> GetAll()
        {
            var Conductores=(from conductor in db.Conductors select conductor).ToList();
            return Conductores;
        }

        [HttpPost]
        public ActionResult Post(Conductor conductor)
        {
            if (conductor.Nombres == "" || conductor.Licencia == "null" || conductor.Nidentificacion == "")
            {
                return BadRequest("Se Encontaron Campos Vacios Requeridos");
            }
                try
                {
                    db.Conductors.Add(conductor);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
                
            return Ok(conductor);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var Conductor = db.Conductors.Find(id);

            if (Conductor == null)
            {
                return NotFound();
            }
            return Ok(Conductor);
        }

        [HttpPut("{id}")]
        public IActionResult PutConductor(int id, Conductor conductor)
        {
            Conductor? ConductorDb = (from Conductor in db.Conductors
                                      where Conductor.IdConductor.Equals(id)
                                      select Conductor).FirstOrDefault();
            if(ConductorDb == null)
            {
                return NotFound();
            }
            else
            {
                ConductorDb.Nombres = conductor.Nombres;
                ConductorDb.Apellidos = conductor.Apellidos;
                ConductorDb.Nidentificacion = conductor.Nidentificacion;
                ConductorDb.Ntelefono = conductor.Ntelefono;
                ConductorDb.Correo=conductor.Correo;
                ConductorDb.Licencia=conductor.Licencia;
                ConductorDb.IdUsuarioRegistro = conductor.IdUsuarioRegistro;
                db.SaveChanges();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConductor(int id)
        {
            Conductor eliminarDb= new Conductor() { IdConductor = id };
            if (eliminarDb is null)
            {
                return NotFound();
            }
            else
            {
                db.Conductors.Remove(eliminarDb);
                db.SaveChanges();
                return Ok();
            }
        }

    }
}
