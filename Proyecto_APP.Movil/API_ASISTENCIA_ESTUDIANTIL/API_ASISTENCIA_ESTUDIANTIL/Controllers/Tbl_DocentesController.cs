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
    public class Tbl_DocentesController : ControllerBase
    {
        private readonly CN_DOCENTES _CN_Docentes;

        int resultado;
        string mensaje;

        CE_DOCENTES _CE_DOCENTES = new CE_DOCENTES();

        public Tbl_DocentesController(IConfiguration configuration)
        {
            _CN_Docentes = new CN_DOCENTES(configuration);
        }

        [HttpGet("OBTENER_DOCENTES")]

        public ActionResult<List<CE_DOCENTES>> Listar_Docentes()
        {
            var resultado = _CN_Docentes.CN_Listar_Docentes();

            if(resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }

        }

        [HttpGet("BUSCAR_DOCENTES_POR_ID")]

        public ActionResult<List<CE_DOCENTES>> Buscar_DocentesID([FromHeader] int? Id_Docente)
        {
            _CE_DOCENTES.Id_Docente = Id_Docente;

            var resultado = _CN_Docentes.CN_Filtrar_DocentesID(_CE_DOCENTES);

            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }

        }

        [HttpGet("BUSCAR_DOCENTES")]

        public ActionResult<List<CE_DOCENTES>> Buscar_Docentes([FromHeader] int? Id_Docente)
        {
            _CE_DOCENTES.Id_Docente = Id_Docente;

            var resultado = _CN_Docentes.CN_Filtrar_Docentes(_CE_DOCENTES);

            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }

        }

        [HttpPost("INSERTAR_DOCENTES")]

        public ActionResult Insertar_Docentes([FromBody] ApiModels.AM_DOCENTES _AM_Docentes)
        {
            _CE_DOCENTES.Id_Persona = _AM_Docentes.Id_Persona;
            _CE_DOCENTES.Id_Creador = _AM_Docentes.Id_Creador;
            _CE_DOCENTES.Id_Estado = _AM_Docentes.Id_Estado;

            try 
            {
                _CN_Docentes.CN_Insertar_Docente(_CE_DOCENTES);
                return Ok("DOCENTE INSERTADO CORRECTAMENTE");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }

        [HttpPut("ACTUALIZAR_DOCENTES")]

        public ActionResult Actualizar_Docentes([FromBody] ApiModels.AM_DOCENTES _AM_Docentes)
        {
            _CE_DOCENTES.Id_Docente = _AM_Docentes.Id_Docente;
            _CE_DOCENTES.Id_Persona = _AM_Docentes.Id_Persona;
            _CE_DOCENTES.Id_Modificador = _AM_Docentes.Id_Modificador;
            _CE_DOCENTES.Id_Estado = _AM_Docentes.Id_Estado;

            AM_RESULTADO _RESULTADO = new AM_RESULTADO();

            try
            {
                _CN_Docentes.CN_Actualizar_Docente(_CE_DOCENTES, out resultado, out mensaje);
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
