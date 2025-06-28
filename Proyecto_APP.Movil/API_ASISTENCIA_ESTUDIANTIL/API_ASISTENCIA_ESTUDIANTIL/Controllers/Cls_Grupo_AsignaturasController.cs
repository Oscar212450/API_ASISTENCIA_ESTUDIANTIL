using API_ASISTENCIA_ESTUDIANTIL.ApiModels;
using CAPA_ENTIDADES;
using CAPA_NEGOCIO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_ASISTENCIA_ESTUDIANTIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Cls_Grupo_AsignaturasController : ControllerBase
    {
        private readonly CN_GRUPO_ASIGNATURA _CN_GA;

        int resultado;
        string mensaje;

        CE_GRUPO_ASIGNATURA _CE_GA = new CE_GRUPO_ASIGNATURA();

        public Cls_Grupo_AsignaturasController(IConfiguration configuration)
        {
            _CN_GA = new CN_GRUPO_ASIGNATURA(configuration);    
        }

        [HttpGet("OBTENER_GRUPO_ASIGNATURA")]

        public ActionResult<List<CE_GRUPO_ASIGNATURA>> Listar_Grupo_Asignatura()
        {
            var resultado = _CN_GA.CN_Listar_Grupo_Asignatura();

            if(resultado.Count == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }
        }

        [HttpGet("BUSCAR_GRUPO_ASIGNATURA")]

        public ActionResult<List<CE_GRUPO_ASIGNATURA>> FiltrarGrupoAsignatura([FromHeader] int? Id_Grupo)
        {
            _CE_GA.Id_Grupo = Id_Grupo;

            var resultado = _CN_GA.CN_Filtrar_Grupo_Asignatura(_CE_GA);

            if (resultado.Count == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }
        }

        [HttpGet("BUSCAR_GRUPO_ASIGNATURA_POR_ID")]

        public ActionResult<List<CE_GRUPO_ASIGNATURA>> FiltrarGrupoAsignaturaID([FromHeader] int? Id_Grupo_Asignatura)
        {
            _CE_GA.Id_Grupo_Asignatura = Id_Grupo_Asignatura;

            var resultado = _CN_GA.CN_Filtrar_Grupo_Asignatura(_CE_GA);

            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }
        }

        [HttpPost("INSERTAR_GRUPO_ASIGNATURA")]

        public ActionResult InsertarGrupoAsignatura([FromBody] ApiModels.AM_GRUPO_ASIGNATURA _am_GA)
        {
            _CE_GA.Id_Grupo = _am_GA.Id_Grupo;
            _CE_GA.Id_Asignatura = _am_GA.Id_Asignatura;
            _CE_GA.Id_Creador = _am_GA.Id_Creador;
            _CE_GA.Id_Estado = _am_GA.Id_Estado;

            try
            {
                _CN_GA.CN_Insertar_Grupo_Asignatura(_CE_GA);

                return Ok("GRUPO_ASIGNATURA_INSERTADO_CORRECTAMENTE");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("ACTUALIZAR_GRUPO_ASIGNATURA")]

        public ActionResult ActualizarGrupoAsignatura([FromBody] ApiModels.AM_GRUPO_ASIGNATURA _am_GA)
        {
            _CE_GA.Id_Grupo_Asignatura = _am_GA.Id_Grupo_Asignatura;
            _CE_GA.Id_Grupo = _am_GA.Id_Grupo;
            _CE_GA.Id_Asignatura = _am_GA.Id_Asignatura;
            _CE_GA.Id_Modificador = _am_GA.Id_Modificador;
            _CE_GA.Id_Estado = _am_GA.Id_Estado;

            AM_RESULTADO _RESULTADO = new AM_RESULTADO();

            try
            {
                _CN_GA.CN_Actualizar_Grupo_Asignatura(_CE_GA, out resultado, out mensaje);
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
