using API_ASISTENCIA_ESTUDIANTIL.ApiModels;
using CAPA_ENTIDADES;
using CAPA_NEGOCIO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_ASISTENCIA_ESTUDIANTIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Cls_Docente_GrupoController : ControllerBase
    {
        private readonly CN_DOCENTE_GRUPO _CN_DG;

        int resultado;
        string mensaje;

        CE_DOCENTE_GRUPO _CE_DG = new CE_DOCENTE_GRUPO();

        public Cls_Docente_GrupoController(IConfiguration configuration)
        {
            _CN_DG = new CN_DOCENTE_GRUPO(configuration);    
        }

        [HttpGet("OBTENER_DOCENTE_GRUPO")]

        public ActionResult<List<CE_DOCENTE_GRUPO>> Listar_Docente_Grupo()
        {
            var resultado = _CN_DG.CN_Listar_Docente_Grupo();

            if(resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }
        }

        [HttpGet("BUSCAR_DOCENTE_GRUPO")]

        public ActionResult<List<CE_DOCENTE_GRUPO>> FiltrarDocenteGrupo([FromHeader] int? Id_Docente)
        {
            _CE_DG.Id_Docente = Id_Docente;

            var resultado = _CN_DG.CN_Filtrar_Docente_Grupo(_CE_DG);

            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }
        }

        [HttpGet("BUSCAR_DOCENTE_GRUPO_POR_ID")]

        public ActionResult<List<CE_DOCENTE_GRUPO>> FiltrarDocenteGrupoID([FromHeader] int? Id_Docente_Grupo)
        {
            _CE_DG.Id_Docente_Grupo = Id_Docente_Grupo;

            var resultado = _CN_DG.CN_Filtrar_Docente_GrupoID(_CE_DG);

            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }
        }
        [HttpPost("INSERTAR_DOCENTE_GRUPO")]

        public ActionResult InsertarDocenteGrupo([FromBody] ApiModels.AM_DOCENTE_GRUPO _am_DG)
        {
            _CE_DG.Id_Docente = _am_DG.Id_Docente;
            _CE_DG.Id_Grupo = _am_DG.Id_Grupo;
            _CE_DG.Id_Creador = _am_DG.Id_Creador;
            _CE_DG.Id_Estado = _am_DG.Id_Estado;

            try
            {
                _CN_DG.CN_Insertar_Docente_Grupo(_CE_DG);

                return Ok("DOCENTE_GRUPO_INSERTADO_CORRECTAMENTE");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("ACTUALIZAR_DOCENTE_GRUPO")]

        public ActionResult ActualizarDocenteGrupo([FromBody] ApiModels.AM_DOCENTE_GRUPO _am_DG)
        {
            _CE_DG.Id_Docente_Grupo = _am_DG.Id_Docente_Grupo;
            _CE_DG.Id_Docente = _am_DG.Id_Docente;
            _CE_DG.Id_Grupo = _am_DG.Id_Grupo;
            _CE_DG.Id_Modificador = _am_DG.Id_Modificador;
            _CE_DG.Id_Estado = _am_DG.Id_Estado;

            AM_RESULTADO _RESULTADO = new AM_RESULTADO();

            try
            {
                _CN_DG.CN_Actualizar_Docente_Grupo(_CE_DG, out resultado,out mensaje);
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
