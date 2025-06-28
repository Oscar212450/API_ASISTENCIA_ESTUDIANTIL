using API_ASISTENCIA_ESTUDIANTIL.ApiModels;
using CAPA_ENTIDADES;
using CAPA_NEGOCIO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_ASISTENCIA_ESTUDIANTIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Cls_Estudiante_GrupoController : ControllerBase
    {
        private readonly CN_ESTUDIANTE_GRUPO _CN_EG;

        int resultado;
        string mensaje;

        CE_ESTUDIANTE_GRUPO _CE_EG = new CE_ESTUDIANTE_GRUPO();

        public Cls_Estudiante_GrupoController(IConfiguration configuration)
        {
            _CN_EG = new CN_ESTUDIANTE_GRUPO(configuration);    
        }

        [HttpGet("OBTENER_ESTUDIANTE_GRUPO")]

        public ActionResult<List<CE_ESTUDIANTE_GRUPO>> Listar_Estudiante_Grupo()
        {
            var resultado = _CN_EG.CN_Listar_Estudiante_Grupo();

            if(resultado.Count == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }
        }

        [HttpGet("OBTENER_ESTUDIANTE_GRUPO_POR_ID")]

        public ActionResult<List<CE_ESTUDIANTE_GRUPO>> FiltrarEstudianteGrupo([FromHeader] int? Id_Estudiante_Grupo)


        {
            _CE_EG.Id_Estudiante_Grupo = Id_Estudiante_Grupo;

            var resultado = _CN_EG.CN_Filtrar_Estudiante_GrupoID(_CE_EG);

            if (resultado.Count == 0 )
            {
                return 
            NoContent();
            }
            else
            {
                return Ok(resultado);
            }
        }

        [HttpGet("BUSCAR_ESTUDIANTE_GRUPO")]

        public ActionResult<List<CE_ESTUDIANTE_GRUPO>> FiltrarEstudianteGrupoID([FromHeader] int? Id_Estudiante)
        {
            _CE_EG.Id_Estudiante = Id_Estudiante;

            var resultado = _CN_EG.CN_Filtrar_Estudiante_Grupo(_CE_EG);

            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }
        }

        [HttpPost("INSERTAR_ESTUDIANTE_GRUPO")]

        public ActionResult InsertarEstudianteGrupo([FromBody] ApiModels.AM_ESTUDIANTE_GRUPO _am_EG)
        {
            _CE_EG.Id_Estudiante = _am_EG.Id_Estudiante;
            _CE_EG.Id_Grupo = _am_EG.Id_Grupo;
            _CE_EG.Id_Creador = _am_EG.Id_Creador;
            _CE_EG.Id_Estado = _am_EG.Id_Estado;

            AM_RESULTADO _RESULTADO = new AM_RESULTADO();

            try
            {
                _CN_EG.CN_Insertar_Estudiante_Grupo(_CE_EG, out int resultdado, out string mensaje);
                _RESULTADO.resultado = resultdado;
                _RESULTADO.mensaje = mensaje;
                return Ok(_RESULTADO);
            }
            catch (Exception ex)
            {
                return BadRequest(_RESULTADO);
            }

        }

        [HttpPut("ACTUALIZAR_ESTUDIANTE_GRUPO")]

        public ActionResult ActualizarEstudianteGrupo([FromBody] ApiModels.AM_ESTUDIANTE_GRUPO _am_EG)
        {
            _CE_EG.Id_Estudiante_Grupo = _am_EG.Id_Estudiante_Grupo;
            _CE_EG.Id_Estudiante = _am_EG.Id_Estudiante;
            _CE_EG.Id_Grupo = _am_EG.Id_Grupo;
            _CE_EG.Id_Creador = _am_EG.Id_Creador;
            _CE_EG.Id_Estado = _am_EG.Id_Estado;

            AM_RESULTADO _RESULTADO = new AM_RESULTADO();

            try
            {
                _CN_EG.CN_Actualizar_Estudiante_Grupo(_CE_EG, out resultado, out mensaje);
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
