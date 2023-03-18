using Microsoft.AspNetCore.Mvc;
using ProyectoApi.Models;

namespace ProyectoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UnidadesTransporteContext db = new UnidadesTransporteContext();

        [HttpGet]
        public IEnumerable<Object> GetAll()
        {
            var Usuarios = (from usuario in db.Usuarios
                            select new
                            {
                                usuario.IdUsuario,
                                usuario.Nombre,
                                usuario.Estado,
                                usuario.Correo
                            }).ToList();
            return Usuarios;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Usuario? usuariodb = (from usuario in db.Usuarios
                                  where usuario.IdUsuario.Equals(id)
                                  select usuario).FirstOrDefault();
            if (usuariodb == null)
            {
                return NotFound();
            }
            return Ok(usuariodb);
        }

        [HttpPost]
        public IActionResult Post(Usuario usuario)
        {
            if (usuario.Nombre == "" || usuario.Correo == "")
            {
                return BadRequest("Se Encontraron Campos Vacios");
            }
            var existetipoUsuario = (from existe in db.TipoUsuarios
                                     where existe.IdTipoUsuario.Equals(usuario.IdTipoUsuario)
                                     select existe).FirstOrDefault();
            if (existetipoUsuario == null)
            {
                return BadRequest($"No Existe el Tipo de Usuario con el ID:{usuario.IdTipoUsuario}");
            }

            try
            {

                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return Ok(usuario);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Usuario eliminardb = new Usuario() { IdUsuario = id };
            if (eliminardb is null)
            {
                return NotFound();
            }
            db.Usuarios.Remove(eliminardb);
            db.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id,Usuario usuario)
        {
            Usuario? usuaridb=(from usuarioU in db.Usuarios
                               where usuarioU.IdUsuario.Equals(id)
                               select usuarioU).FirstOrDefault();

            var existeTipo = db.TipoUsuarios.Find(id);

            if(existeTipo is null)
            {
                return BadRequest($"No existe el Tipo de Usuario con el ID:{id}");
            }

            if (usuaridb.Nombre == "" || usuaridb.Correo == "")
            {
                return BadRequest("Se Encontaron Campos Vacios");
            
            }
            else
            {
                usuaridb.Nombre = usuario.Nombre;
                usuaridb.Contrasenia= usuario.Contrasenia;
                usuaridb.Correo= usuario.Correo;
                usuaridb.Estado= usuario.Estado;
                usuaridb.IdTipoUsuario = usuario.IdTipoUsuario;
                db.SaveChanges();
                return Ok();
            }
        }
    }
}
