using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProyectoApi.DTOs;
using ProyectoApi.Models;

namespace ProyectoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadesController : ControllerBase
    {
        private readonly UnidadesTransporteContext db = new UnidadesTransporteContext();
        private readonly IMapper _mapper;

        [HttpGet]
        public IEnumerable<Object> GetAll()
        {
            var unidades = (from unidad in db.Unidades
                            select  unidad).ToList();
            return unidades;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Unidad? unidad = (from unidades in db.Unidades
                              where unidades.IdUnidad.Equals(id)
                              select unidades).FirstOrDefault();
            if (unidad is null)
            {
                return NotFound("No Se Encontro Registro con el ID Proporcionado...");
            }

            return Ok(unidad);
        }

        [HttpPost]
        public IActionResult Post(UnidadPostDto unidad)
        {
            if(unidad is null)
            {
                return BadRequest("El Registro Enviado se Encuentra Vacio");
            }

            if (unidad.Nplaca == String.Empty || unidad.Descripcion == String.Empty || unidad.Marca == String.Empty)
            {
                return BadRequest("Se Encontraron Campos Requeridos Vacios..");
            }

            try
            {
                Unidad unidadb = _mapper.Map<Unidad>(unidad);   
                db.Unidades.Add(unidadb);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

            return Ok("Registro Exitoso....");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Unidad unidad)
        {
            Unidad? unidadb = (from unidadid in db.Unidades
                               where unidadid.IdUnidad.Equals(id)
                               select unidadid).FirstOrDefault();
            if(unidadb is null)
            {
                return BadRequest("No se encontro Registro con el Id Proporcionado");
            }
            unidadb.Nplaca = unidad.Nplaca;
            unidadb.Marca=unidad.Marca;
            unidadb.Modelo = unidad.Modelo;
            unidadb.Descripcion = unidad.Descripcion;
            db.SaveChanges();
            return Ok(unidadb);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Unidad Eliminardb = new Unidad() { IdUnidad = id };
            db.Unidades.Remove(Eliminardb);
            db.SaveChanges();
            return Ok("Eliminacion Exitosa...");
        }
    }
}
