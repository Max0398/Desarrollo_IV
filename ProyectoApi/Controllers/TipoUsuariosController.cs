using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoApi.DTOs;
using ProyectoApi.Models;

namespace ProyectoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuariosController : ControllerBase
    {
        private readonly UnidadesTransporteContext db = new UnidadesTransporteContext();
        private readonly IMapper _mapper;
        public TipoUsuariosController(IMapper mapper)
        {
            _mapper = mapper;
        }


        [HttpGet]
        public IEnumerable<Object> Get()
        {
            var tiposUsuarios=( from tipos in db.TipoUsuarios
                               select new 
                               {
                                   tipos.IdTipoUsuario,
                                   tipos.Tipo,
                                   tipos.Descripcion
                               }).ToList(); 
            return tiposUsuarios;   
        }

        [HttpPost]
        public IActionResult Post(TipoUsuarioPostDto tipo)
        {
            if (tipo == null)
            {
                return BadRequest("El campo Tipo es requerido");
            }
            try
            {
                TipoUsuario tipodb = _mapper.Map<TipoUsuario>(tipo);
                db.TipoUsuarios.Add(tipodb);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            return Ok("Tipo de Usuario Registrado Correctamente");
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {

          TipoUsuario tipo =new TipoUsuario (){ IdTipoUsuario = id };

            db.TipoUsuarios.Remove(tipo);

            db.SaveChanges();

            return Ok("Se Elimino Correctamente");
        }

        [HttpGet]
        [Route("{tipo}")]
        public IActionResult GetNombreTipo(string tipo)
        {
            var tiposUsuarios = ( from tipos in db.TipoUsuarios
                                 where tipos.Tipo.Equals(tipo)
                                 select new {
                                 tipos.Tipo,
                                 tipos.Descripcion
                                 }).FirstOrDefault();

            if (tiposUsuarios == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(tiposUsuarios);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            var tiposUsuarios = (from tipos in db.TipoUsuarios
                                 where tipos.Tipo.Equals(id)
                                 select new
                                 {
                                     tipos.Tipo,
                                     tipos.Descripcion
                                 }).FirstOrDefault();

            if (tiposUsuarios == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(tiposUsuarios);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(int id,TipoUsuario tipo)
        {
            TipoUsuario? tipoDB = (from tipos in db.TipoUsuarios
                                  where tipos.IdTipoUsuario.Equals(id)
                                  select tipos).FirstOrDefault();
            if (tipoDB == null)
                return NotFound();
            else
            {
                tipoDB.Tipo = tipo.Tipo;
                tipoDB.Descripcion = tipo.Descripcion;
                db.SaveChanges();

            }
            return Ok();    

        }

    }
}
