using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CAPA_NEGOCIO;
using CAPA_ENTIDADES;
using API_ASISTENCIA_ESTUDIANTIL.ApiModels;

namespace API_ASISTENCIA_ESTUDIANTIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Cls_HorariosController : ControllerBase
    {
        private readonly CN_HORARIOS _CNHorarios;

        int resultado;
        string mensaje;

        CE_HORARIOS _CEHorarios = new CE_HORARIOS();

        public Cls_HorariosController(IConfiguration configuration)
        {
            _CNHorarios = new CN_HORARIOS(configuration);
        }

        [HttpGet("OBTENER_HORARIOS")]
        public ActionResult<List<CE_HORARIOS>> ListarHorarios()
        {
            var resultado = _CNHorarios.CN_Listar_Horarios();
 
           if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }
           
        }

        [HttpGet("BUSCAR_HORARIOS_POR_ID")]
        public ActionResult<List<CE_HORARIOS>> FiltrarHorariosID([FromHeader] string? Id_Horario)
        {

            _CEHorarios.Horario = Id_Horario;

            var resultado = _CNHorarios.CN_Filtrar_Horarios(_CEHorarios);
            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }

        }

        [HttpGet("BUSCAR_HORARIOS")]
        public ActionResult<List<CE_HORARIOS>> FiltrarHorarios([FromHeader] string? Horario)
        {
            
            _CEHorarios.Horario = Horario;

            var resultado = _CNHorarios.CN_Filtrar_Horarios(_CEHorarios);
            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }

        }
        [HttpPost("INSERTAR_HORARIO")]
        public ActionResult InsertarHorario([FromBody] ApiModels.AM_HORARIOS _Am_Horarios)
        {
            _CEHorarios.Id_Turno = _Am_Horarios.Id_Turno;
            _CEHorarios.Horario = _Am_Horarios.Horario;
            _CEHorarios.Hora_Inicio = _Am_Horarios.Hora_Inicio;
            _CEHorarios.Hora_Fin = _Am_Horarios.Hora_Fin;
            _CEHorarios.Id_Creador = _Am_Horarios.Id_Creador;
            _CEHorarios.Id_Estado = _Am_Horarios.Id_Estado;

            try
            {
                _CNHorarios.CN_Insertar_Horarios(_CEHorarios);
                return Ok("HORARIO INSERTADO CORRECTAMENTE");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            
        }

        [HttpPut("ACTUALIZAR_HORARIO")]

        public ActionResult ActualizarHorario([FromBody] ApiModels.AM_HORARIOS _Am_Horarios)
        {
            _CEHorarios.Id_Horario = _Am_Horarios.Id_Horario;
            _CEHorarios.Id_Turno = _Am_Horarios.Id_Turno;
            _CEHorarios.Horario = _Am_Horarios.Horario;
            _CEHorarios.Hora_Inicio = _Am_Horarios.Hora_Inicio;
            _CEHorarios.Hora_Fin = _Am_Horarios.Hora_Fin;
            _CEHorarios.Id_Creador = _Am_Horarios.Id_Creador;
            _CEHorarios.Id_Estado = _Am_Horarios.Id_Estado;

            AM_RESULTADO _RESULTADO = new AM_RESULTADO();

            try
            {
                _CNHorarios.CN_Actualizar_Horarios(_CEHorarios, out resultado, out mensaje);
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
