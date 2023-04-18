using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProyectoApi.DTOs;
using ProyectoApi.Models;

namespace ProyectoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinoController : ControllerBase
    {
        private readonly UnidadesTransporteContext db= new UnidadesTransporteContext();
        private readonly IMapper _mapper;

        public DestinoController(IMapper mapper)
        {
            _mapper = mapper;
        }



        [HttpGet]
        public IEnumerable<Object> GetAll()
        {
            return db.Destinos.ToList();
        }

        [HttpGet("{id}")]
        public  IActionResult GetById(int id)
        {
           Destino? destinodb = (from destino in db.Destinos
                               where destino.IdDestino.Equals(id)
                               select destino).FirstOrDefault();
         if(destinodb == null)
            {
                return NotFound($"No se Encontraron Registros con ID:{id}.");
            }
         return Ok(destinodb);
        }

        [HttpPost]
        public IActionResult Post(DestinoPostDto destino)
        {
            if (destino == null)
            {
                return BadRequest("Error Registro Vacio");
            }
            if (destino.Descripcion ==string.Empty || destino.Direccion ==string.Empty)
            {
                return BadRequest("Hay Campos Obligatorios Vacios");
            }
          
            try
            {
                Destino destinodb = _mapper.Map<Destino>(destino);

                db.Destinos.Add(destinodb);
                db.SaveChanges();
            }
            catch (Exception Ex )
            {
                return BadRequest(Ex+"?");

            }
            return Ok("Registro Exitoso");

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id,Destino destino)
        {
            Destino? destinodb = (from destinod in db.Destinos
                                  where destinod.IdDestino.Equals(id)
                                  select destinod).FirstOrDefault();
            if (id != destino.IdDestino)
            {
                return BadRequest($"El ID:{id} No Coincide con el ID:{destino.IdDestino} del Registro.");
            }
            if (destino == null)
            {
                return NotFound();
            }

            destinodb.Descripcion = destino.Descripcion;
            destinodb.Direccion = destino.Direccion;
            destinodb.IdDepartamento = destino.IdDepartamento;
            destinodb.IdMunicipio = destino.IdMunicipio;
            destinodb.IdUnidad = destino.IdUnidad;
            destinodb.IdConductor = destino.IdConductor;
            destinodb.IdTipoViaje = destino.IdTipoViaje;
            db.SaveChanges();
            return Ok(destinodb);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Destino? eliminar = new Destino() { IdDestino = id };

            if (eliminar is null)
            {
                return BadRequest($"No se encontro Registro Con el ID: {id} que fue Proporcionado");

            }
            db.Destinos.Remove(eliminar);
            db.SaveChanges();
            return Ok();
        }
    }
}
