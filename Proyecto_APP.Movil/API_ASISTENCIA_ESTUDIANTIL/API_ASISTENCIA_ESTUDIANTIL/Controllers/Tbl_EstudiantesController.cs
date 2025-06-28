using API_ASISTENCIA_ESTUDIANTIL.ApiModels;
using CAPA_DATOS;
using CAPA_ENTIDADES;
using CAPA_NEGOCIO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_ASISTENCIA_ESTUDIANTIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Tbl_EstudiantesController : ControllerBase
    {
        private readonly CN_ESTUDIANTES _CN_Estudiantes;

        int resultado;
        string mensaje;

        CE_ESTUDIANTES _CE_ESTUDIANTES = new CE_ESTUDIANTES();

        public Tbl_EstudiantesController (IConfiguration configuration)
        {
            _CN_Estudiantes = new CN_ESTUDIANTES(configuration);
        }

        [HttpGet("OBTENER_ESTUDIANTES")]

        public ActionResult<List<CE_ESTUDIANTES>> Listar_Estudiantes()
        {
            var resultado = _CN_Estudiantes.CN_Listar_Estudiantes();

            if(resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }

        }
        [HttpGet("BUSCAR_ESTUDIANTES_POR_ID")]

        public ActionResult<List<CE_ESTUDIANTES>> Buscar_EstudiantesID([FromHeader] int? Id_Estudiante)
        {
            _CE_ESTUDIANTES.Id_Estudiante = Id_Estudiante;

            var resultado = _CN_Estudiantes.CN_Filtrar_Estudiantes(_CE_ESTUDIANTES);

            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }

        }

        [HttpGet("BUSCAR_ESTUDIANTES")]

        public ActionResult<List<CE_ESTUDIANTES>> Buscar_Estudiantes([FromHeader] int? Id_Estudiante)
        {
            _CE_ESTUDIANTES.Id_Estudiante = Id_Estudiante;

            var resultado = _CN_Estudiantes.CN_Filtrar_Estudiantes(_CE_ESTUDIANTES);

            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }

        }

        [HttpPost("INSERTAR_ESTUDIANTE")]

        public ActionResult Insertar_Estudiantes([FromBody] ApiModels.AM_ESTUDIANTES _AM_Estudiantes)
        {
            _CE_ESTUDIANTES.Id_Persona = _AM_Estudiantes.Id_Persona;
            _CE_ESTUDIANTES.Id_Creador = _AM_Estudiantes.Id_Creador;
            _CE_ESTUDIANTES.Id_Estado = _AM_Estudiantes.Id_Estado;

            try 
            {
                _CN_Estudiantes.CN_Insertar_Estudiante(_CE_ESTUDIANTES);
                return Ok("ESTUDIANTE INSERTADO CORRECTAMENTE");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }

        [HttpPut("ACTUALIZAR_ESTUDIANTE")]

        public ActionResult Actualizar_Estudiantes([FromBody] ApiModels.AM_ESTUDIANTES _AM_Estudiantes)
        {
            _CE_ESTUDIANTES.Id_Estudiante = _AM_Estudiantes.Id_Estudiante;
            _CE_ESTUDIANTES.Id_Persona = _AM_Estudiantes.Id_Persona;
            _CE_ESTUDIANTES.Id_Modificador = _AM_Estudiantes.Id_Modificador;
            _CE_ESTUDIANTES.Id_Estado = _AM_Estudiantes.Id_Estado;

             AM_RESULTADO _RESULTADO = new AM_RESULTADO();

            try
            {
                _CN_Estudiantes.CN_Actualizar_Estudiante(_CE_ESTUDIANTES, out resultado, out mensaje);
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
