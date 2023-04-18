using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoApi.DTOs;
using ProyectoApi.Models;

namespace ProyectoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoViajeController : ControllerBase
    {
        private readonly UnidadesTransporteContext db = new UnidadesTransporteContext();
        private readonly IMapper _mapper;
        public TipoViajeController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<Object> GetAll()
        {
            var Tviajes = (from tviaje in db.TipoViajes
                           select tviaje).ToList();
            return Tviajes;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            TipoViaje? tviaje = (from tviajeid in db.TipoViajes
                                 where tviajeid.IdTipoViaje.Equals(id)
                                 select tviajeid).FirstOrDefault();
            if (tviaje is null)
            {
                return NotFound("No se Encontraron Registros..");
            }

            return Ok(tviaje);
        }

        [HttpPost]
        public IActionResult Post(TipoViajePostDto tipoViaje)
        {
            if (tipoViaje is null)
            {
                return BadRequest("El Registro Enviado Esta vacio..");
            }
            if (tipoViaje.Descripcion is null)
            {
                return BadRequest("Se Encontro un Capo Requerido Vacio");
            }

            try
            {
                TipoViaje tipoViajedb=_mapper.Map<TipoViaje>(tipoViaje);
                db.TipoViajes.Add(tipoViajedb);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok("Registro Exitoso..");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, TipoViaje tipoViaje)
        {
            TipoViaje? tipoviajesdb = (from tviaje in db.TipoViajes
                                       where tviaje.IdTipoViaje.Equals(id)
                                       select tviaje).FirstOrDefault();

            if (tipoviajesdb is null)
            {
                return NotFound($"No se Encontro Registro con el ID: {id}...");
            }

            tipoviajesdb.Descripcion = tipoViaje.Descripcion;
            db.SaveChanges();

            return Ok(tipoviajesdb);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TipoViaje? tviaje = new TipoViaje() { IdTipoViaje = id };

            db.TipoViajes.Remove(tviaje);
            db.SaveChanges();
            return Ok();

        }

    }
}
