using API_ASISTENCIA_ESTUDIANTIL.ApiModels;
using CAPA_ENTIDADES;
using CAPA_NEGOCIO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_ASISTENCIA_ESTUDIANTIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Tbl_UsuariosController : ControllerBase
    {
        private readonly CN_USUARIOS _CN_Usuarios;

        int resultado;
        string mensaje;

        CE_USUARIOS _CE_USuarios = new CE_USUARIOS();

        public Tbl_UsuariosController(IConfiguration configuration)
        {
            _CN_Usuarios = new CN_USUARIOS(configuration);
        }

        [HttpGet("OBTENER_USUARIOS")]

        public ActionResult<List<CE_USUARIOS>> ListarUsuarios()
        {
            var resultado = _CN_Usuarios.CN_Listar_Usuarios();

            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }
        }

        [HttpGet("BUSCAR_USUARIOS_POR_ID")]
        public ActionResult<List<CE_USUARIOS>> FiltrarUsuarios([FromHeader] int? Id_Usuario)
        {

            _CE_USuarios.Id_Usuario = Id_Usuario;


            var resultado = _CN_Usuarios.CN_Filtrar_UsuariosID(_CE_USuarios);
            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }

        }
        [HttpGet("BUSCAR_USUARIOS")]
        public ActionResult<List<CE_USUARIOS>> FiltrarUsuarios([FromHeader] string? Usuario)
        {

            _CE_USuarios.Usuario = Usuario;


            var resultado = _CN_Usuarios.CN_Filtrar_Usuarios(_CE_USuarios);
            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }

        }

        [HttpPost("INSERTAR_USUARIO")]
        public ActionResult InsertarUsuarios([FromBody] ApiModels.AM_USUARIOS _am_Usuarios)
        {
            _CE_USuarios.Id_Persona = _am_Usuarios.Id_Persona;
            _CE_USuarios.Usuario = _am_Usuarios.Usuario;
            _CE_USuarios.Contrasena = _am_Usuarios.Contrasena;
            _CE_USuarios.Ultima_Sesion = _am_Usuarios.Ultima_Sesion;
            _CE_USuarios.Ultima_Cambio_Credenciales = _am_Usuarios.Ultima_Cambio_Credenciales;
            _CE_USuarios.Intentos_Sesion = _am_Usuarios.Intentos_Sesion;
            _CE_USuarios.Id_Creador = _am_Usuarios.Id_Creador;
            _CE_USuarios.Id_Estado = _am_Usuarios.Id_Estado;

            AM_RESULTADO _RESULTADO = new AM_RESULTADO();

            try
            {
                _CN_Usuarios.CN_Insertar_Usuarios(_CE_USuarios, out resultado, out mensaje );
                _RESULTADO.resultado = resultado;
                _RESULTADO.mensaje = mensaje;
                return Ok(_RESULTADO);
            }
            catch
            {
                return BadRequest(_RESULTADO);
            }

        }

        [HttpPut("ACTUALIZAR_USUARIO")]
        public ActionResult ActualizarUsuario([FromBody] ApiModels.AM_USUARIOS _am_Usuarios)
        {
            _CE_USuarios.Id_Usuario = _am_Usuarios.Id_Usuario;
            _CE_USuarios.Id_Persona = _am_Usuarios.Id_Persona;
            _CE_USuarios.Usuario = _am_Usuarios.Usuario;
            _CE_USuarios.Contrasena = _am_Usuarios.Contrasena;
            _CE_USuarios.Ultima_Sesion = _am_Usuarios.Ultima_Sesion;
            _CE_USuarios.Ultima_Cambio_Credenciales = _am_Usuarios.Ultima_Cambio_Credenciales;
            _CE_USuarios.Intentos_Sesion = _am_Usuarios.Intentos_Sesion;
            _CE_USuarios.Id_Creador = _am_Usuarios.Id_Creador;
            _CE_USuarios.Id_Estado = _am_Usuarios.Id_Estado;

            AM_RESULTADO _RESULTADO = new AM_RESULTADO();

            try
            {
                _CN_Usuarios.CN_Insertar_Usuarios(_CE_USuarios, out resultado, out mensaje);
                _RESULTADO.resultado = resultado;
                _RESULTADO.mensaje = mensaje;
                return Ok(_RESULTADO);
            }
            catch
            {
                return BadRequest(_RESULTADO);
            }

        }
    }
}
