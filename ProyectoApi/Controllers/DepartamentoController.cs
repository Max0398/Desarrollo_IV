using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoApi.Models;

namespace ProyectoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly UnidadesTransporteContext db = new UnidadesTransporteContext();

        [HttpGet]
        public IEnumerable<object> Get()
        {
            return db.Departamentos.ToList();//mas corto para devolver todos lo registros
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Departamento? departamento = (from departamentoId in db.Departamentos
                                          where departamentoId.IdDepartamento.Equals(id)
                                          select departamentoId).FirstOrDefault();
            if(departamento == null)
            {
                return NotFound($"No se Encontro el Departamento con el ID:{id}");
            }

            return Ok(departamento);
        }

        [HttpPost]
        public IActionResult Post(Departamento departamento)
        {
            if (departamento.Descripcion== null)
            {
                return BadRequest("Se Encontraron Campos Vacios");
            }
            db.Departamentos.Add(departamento);
            db.SaveChanges();
            return Ok("Guardado Exitoso...");
        }
        [HttpPut("{id}")]
        public IActionResult Put( int id,Departamento departamento)
        {
            Departamento? departamentodb = db.Departamentos.Find(id);
            if (departamentodb == null)
            {
                return BadRequest("No se encontro Datos con el Id Proporcionado..");
            }
            departamentodb.Descripcion = departamento.Descripcion;
            db.SaveChanges();
            return Ok("Modificacion Exitosa");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Departamento? departamentoEliminar = new Departamento() { IdDepartamento = id };
            if (departamentoEliminar is null)
            {
                return BadRequest("No Encontro Registro con el ID Enviado..");
            }

            db.Departamentos.Remove(departamentoEliminar);
            db.SaveChanges();
            return Ok($"Se Elimino el Registro con el ID:{id}");
        }
    }
}
