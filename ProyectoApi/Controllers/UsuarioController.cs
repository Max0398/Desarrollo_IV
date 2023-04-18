using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProyectoApi.DTOs;
using ProyectoApi.Models;
using ProyectoApi.Utilidades;

namespace ProyectoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UnidadesTransporteContext db = new UnidadesTransporteContext();
        private readonly IMapper _mapper;
        public UsuarioController(IMapper mapper )
        {
            _mapper = mapper;
        }

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
        public IActionResult Post(UsuarioPostDto usuario)
        {

            if (usuario.Nombre == "" || usuario.Correo == "")
            {
                return BadRequest("Se Encontraron Campos Vacios");
            }
            var existetipoUsuario = (from existe in db.TipoUsuarios
                                     where existe.IdTipoUsuario==usuario.IdTipoUsuario
                                     select existe).FirstOrDefault();
            if (existetipoUsuario == null)
            {
                return BadRequest($"No Existe el Tipo de Usuario con el ID:{usuario.IdTipoUsuario}");
            }

            var correo = (from user in db.Usuarios
                          where user.Correo.Equals(usuario.Correo)
                          select user).FirstOrDefault();
          
            if (correo != null)
            {
                return BadRequest("Correo En Uso..");
            }
         
            //if (!Encriptar.ValidarContrasenia(usuario.Contrasenia))
            //{
            //    return BadRequest("La contraseña debe cumplir con los siguientes requisitos: contener al menos una letra mayúscula, una letra minúscula, un número, un símbolo y tener una longitud mínima de 8 caracteres.");
            //}


            try
            {
                Usuario nuevoUsuario = _mapper.Map<Usuario>(usuario);
                db.Usuarios.Add(nuevoUsuario);
                db.SaveChanges();
                return Ok("Guardado...");

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    // Acceder a los detalles de la excepción interna
                    var innerException = ex.InnerException;
                    var errorMessage = innerException.Message;
                    // Puedes imprimir o registrar el mensaje de error para su revisión
                    Console.WriteLine("Error al guardar los cambios en la entidad: " + errorMessage);
                }
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

        //Login Usuario
        [HttpPost]
        [Route("Login")]
        public IActionResult LoginUsuario(UsuarioLoginDto usuario)
        {
            var usuariodb = (from user in db.Usuarios
                             where user.Nombre.Equals(usuario.NombreUsuario) 
                             select user).FirstOrDefault();
            if (usuariodb is null)
            {
                return NotFound("Usuario No Encontrado");
            }
            if (Encriptar.Check(usuariodb.Contrasenia, usuario.Contrasenia) == true)
            {
                return Ok();
            }
            else
            {
                return Unauthorized("Usuario No Valido...");
            }
        }

    }
}
