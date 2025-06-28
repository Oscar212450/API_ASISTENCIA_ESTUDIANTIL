using API_ASISTENCIA_ESTUDIANTIL.ApiModels;
using CAPA_ENTIDADES;
using CAPA_NEGOCIO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_ASISTENCIA_ESTUDIANTIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Cls_Grupo_TurnoController : ControllerBase
    {
        private readonly CN_GRUPO_TURNO _CN_GT;

        int resultado;
        string mensaje;

        CE_GRUPO_TURNO _CE_GT = new CE_GRUPO_TURNO();

        public Cls_Grupo_TurnoController(IConfiguration configuration)
        {
            _CN_GT = new CN_GRUPO_TURNO(configuration);    
        }

        [HttpGet("OBTENER_GRUPO_TURNO")]

        public ActionResult<List<CE_GRUPO_TURNO>> Listar_Grupo_Turno()
        {
            var resultado = _CN_GT.CN_Listar_Grupo_Turno();

            if(resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }
        }
        [HttpGet("BUSCAR_GRUPO_TURNO_POR_ID")]

        public ActionResult<List<CE_GRUPO_TURNO>> FiltrarGrupoTurnoID([FromHeader] int? Id_Horario)
        {
            _CE_GT.Id_Horario = Id_Horario;

            var resultado = _CN_GT.CN_Filtrar_Grupo_Turno(_CE_GT);

            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }
        }

        [HttpGet("BUSCAR_GRUPO_TURNO")]

        public ActionResult<List<CE_GRUPO_TURNO>> FiltrarGrupoTurno([FromHeader] int? Id_Grupo_Turno)
        {
            _CE_GT.Id_Grupo_Turno = Id_Grupo_Turno;

            var resultado = _CN_GT.CN_Filtrar_Grupo_Turno(_CE_GT);

            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }
        }

        [HttpPost("INSERTAR_GRUPO_TURNO")]

        public ActionResult InsertarGrupoTurno([FromBody] ApiModels.AM_GRUPO_TURNO _am_GT)
        {
            _CE_GT.Id_Grupo = _am_GT.Id_Grupo;
            _CE_GT.Id_Horario = _am_GT.Id_Horario;
            _CE_GT.Id_Creador = _am_GT.Id_Creador;
            _CE_GT.Id_Estado = _am_GT.Id_Estado;

            try
            {
                _CN_GT.CN_Insertar_Grupo_Turno(_CE_GT);

                return Ok("GRUPO_TURNO_INSERTADO_CORRECTAMENTE");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("ACTUALIZAR_GRUPO_TURNO")]

        public ActionResult ActualizarGrupoTurno([FromBody] ApiModels.AM_GRUPO_TURNO _am_GT)
        {
            _CE_GT.Id_Grupo_Turno = _am_GT.Id_Grupo_Turno;
            _CE_GT.Id_Grupo = _am_GT.Id_Grupo;
            _CE_GT.Id_Horario = _am_GT.Id_Horario;
            _CE_GT.Id_Modificador = _am_GT.Id_Modificador;
            _CE_GT.Id_Estado = _am_GT.Id_Estado;

            AM_RESULTADO _RESULTADO = new AM_RESULTADO();

            try
            {
                _CN_GT.CN_Actualizar_Grupo_Turno(_CE_GT, out resultado, out mensaje);
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
