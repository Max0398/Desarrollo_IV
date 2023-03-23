using Microsoft.AspNetCore.Mvc;
using ProyectoApi.Models;

namespace ProyectoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetornoController : ControllerBase
    {
        private readonly UnidadesTransporteContext db = new UnidadesTransporteContext();

        [HttpGet]
        public IEnumerable<Object> GetAll()
        {
            return db.Retornos.ToList();
        }

        [HttpGet("{id}")]
        public  IActionResult GetById(int id)
        {
            Retorno? retorno=(from  retornoId in  db.Retornos
                              where retornoId.IdRetorno.Equals(id)
                              select retornoId).FirstOrDefault();   
            if(retorno == null)
            {
                return NotFound();
            }
            return Ok(retorno);
        }

        [HttpPost]
        public async Task<IActionResult> Put(Retorno retorno)
        {
            if(retorno is null)
            {
                return  BadRequest();
            }

            try
            {
                await db.Retornos.AddAsync(retorno);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>Put (int id, Retorno retorno)
        {
            Retorno? retornodb = (from retorn in db.Retornos
                                  where retorn.IdRetorno.Equals(id)
                                  select retorn).FirstOrDefault();

            if(retornodb is null)
            {
                return BadRequest(" No se Encontro Registro con el ID Proporcionado");
            }
            retornodb.IdDestino = retorno.IdDestino;
            retornodb.Fhretorno = retorno.Fhretorno;
            retornodb.IdUsuarioRegistro = retorno.IdUsuarioRegistro;
            db.SaveChanges();
            return Ok(retornodb);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRetorno(int id)
        {
            Retorno? eliminarDb = new Retorno() { IdRetorno=id };
            if(eliminarDb is null)
            {
                return NotFound();
            }
            db.Retornos.Remove(eliminarDb);
            db.SaveChanges();
            return Ok("Eliminacion Exitosa");
        }


    }

}
