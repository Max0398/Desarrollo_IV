using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProyectoApi.Dto;
using ProyectoApi.DTOs;
using ProyectoApi.Models;

namespace ProyectoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalidaController : ControllerBase
    {
        private readonly UnidadesTransporteContext db = new UnidadesTransporteContext();
        private readonly IMapper _mapper;
        public SalidaController(IMapper mapper)
        {
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var salidas = (from salida in db.Salidas
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
            Salida? salida = (from salidadb in db.Salidas
                              where salidadb.IdSalida.Equals(id)
                              select salidadb).FirstOrDefault();
            if(salida is null)
            {
                return NotFound();
            }
            return Ok(salida);
        }

        [HttpPost]
        public IActionResult Post(SalidaPostDto salida)
        {
            if (salida is null)
            {
                return BadRequest("Registro Vacio..");
            }
            if (salida.IdDestino==0||salida.Fhsalida==null)
            {
                return BadRequest("Se Encontraron Campos Vacios..");
            }

            try
            {

                Salida salidadb = _mapper.Map<Salida>(salida);

                db.Salidas.Add(salidadb);
                db.SaveChanges();

            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Salida salida)
        {
           Salida? salidadb = (from salidaU in db.Salidas
                               where salidaU.IdSalida.Equals (id)
                               select salidaU).FirstOrDefault();    
            if(salidadb is null)
            {
                return BadRequest();
            }
            salidadb.IdDestino = salida.IdDestino;
            salidadb.Fhsalida=salida.Fhsalida;
            db.SaveChanges();
            return Ok(salidadb);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete (int id)
        {
            Salida? eliminardb = new Salida() { IdSalida = id };
            if (eliminardb is null)
            {
                return NotFound();
            }
            db.Salidas.Remove(eliminardb);
            db.SaveChanges();
            return Ok();
        }


    }
}
