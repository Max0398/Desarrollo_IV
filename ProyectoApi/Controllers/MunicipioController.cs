using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoApi.DTOs;
using ProyectoApi.Models;

namespace ProyectoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipioController : ControllerBase
    {
        private readonly UnidadesTransporteContext db = new UnidadesTransporteContext();
        private readonly IMapper _mapper;

        public MunicipioController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<Object> GetAll()
        {
            return db.Municipios.ToList();
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            Municipio? municipio=(from muni in db.Municipios
                                  where muni.IdMunicipio.Equals(id)
                                  select muni).FirstOrDefault();
            if(municipio is null)
            {
                return NotFound($"No hay Registro Con el ID:{id}.");
            }

            return Ok(municipio);

        }

        [HttpPost]
        public IActionResult Put(MunicipioPostDto municipio)
        {
            if(municipio is null)
            {
                return BadRequest();
            }

            if(municipio.Descripcion==String.Empty)
            {
                return BadRequest("Se Encontraron Campos Vacios");
            }

            try
            {
                Municipio municipiodb = _mapper.Map<Municipio>(municipio);

                db.Municipios.Add(municipiodb);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok("Registro Exitoso...");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Municipio municipio)
        {
            Municipio? municipiodb = (from muni in db.Municipios
                                      where muni.IdMunicipio.Equals(id)
                                      select muni).FirstOrDefault();
            if(municipiodb is null)
            {
                return BadRequest("No se Encontro Registro con el ID Proporcionado..");
            }
            municipiodb.Descripcion=municipio.Descripcion;
            db.SaveChanges();
            return Ok(municipiodb);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Municipio? eliminardb = (from municipio in db.Municipios
                                     where municipio.IdMunicipio.Equals(id)
                                     select municipio).FirstOrDefault();
            if(eliminardb is null)
            {
                return BadRequest($"No se Encontro Registro con el ID:{id} Eliminacion Fallida!");
            }
            db.Municipios.Remove(eliminardb);
            db.SaveChanges();
            return Ok();
        }
    }
}
