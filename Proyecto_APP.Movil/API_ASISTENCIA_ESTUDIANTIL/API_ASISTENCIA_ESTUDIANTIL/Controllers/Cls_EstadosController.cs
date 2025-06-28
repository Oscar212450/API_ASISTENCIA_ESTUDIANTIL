using API_ASISTENCIA_ESTUDIANTIL.ApiModels;
using CAPA_ENTIDADES;
using CAPA_NEGOCIO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_ASISTENCIA_ESTUDIANTIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Cls_EstadosController : ControllerBase
    {
        private readonly CN_ESTADOS _CN_Estados;

        int resultado;
        string mensaje;

        CE_ESTADOS _CE_Estados = new CE_ESTADOS();

        public Cls_EstadosController(IConfiguration configuration)
        {
            _CN_Estados = new CN_ESTADOS(configuration);
        }

        [HttpGet("OBTENER_ESTADOS")]
         public ActionResult<List<CE_ESTADOS>> LISTAR_ESTADOS()

        {
            

            var resultado = _CN_Estados.CN_Listar_Estados();

            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }
        }

        [HttpGet("BUSCAR_ESTADOS_POR_ID")]
        public ActionResult<List<CE_ESTADOS>> Buscar_EstadosID([FromHeader] int? Id_Estados)
        {

            _CE_Estados.Id_Estado = Id_Estados;

            var resultado = _CN_Estados.CN_Filtrar_EstadoID(_CE_Estados);

            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }

        }

        [HttpGet("BUSCAR_ESTADOS")]
        public ActionResult<List<CE_ESTADOS>> Buscar_Estados([FromHeader] String? Estados)
        {

            _CE_Estados.Estado = Estados;
            
            var resultado = _CN_Estados.CN_Filtrar_Estado(_CE_Estados);


            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }

        }

        [HttpPost("INSERTAR_Estados")]

        public ActionResult<List<CE_ESTADOS>> Insertar_Estados([FromBody] ApiModels.AM_ESTADOS aM_ESTADOS)
        {

            _CE_Estados.Id_Creador = aM_ESTADOS.Id_Creador;
            _CE_Estados.Fecha_Creacion = aM_ESTADOS.Fecha_Creacion;
            _CE_Estados.Fecha_Modificacion = aM_ESTADOS.Fecha_Modificacion;
            

            try
            {

                return Ok("ESTADO INSERTADO CORRECTAMENTE");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("ACTUALIZAR_ESTADOS")]

        public ActionResult Actualizar_Estados([FromBody] ApiModels.AM_ESTADOS _Am_Estados)
        {
            _CE_Estados.Id_Estado = _Am_Estados.Id_Estados;
            _CE_Estados.Estado = _Am_Estados.Estado;
            _CE_Estados.Id_Modificador = _Am_Estados.Id_Modificador;
            _CE_Estados.Activo = _Am_Estados.Activo;
             
            AM_RESULTADO _RESULTADO = new AM_RESULTADO();

            try
            {
                _CN_Estados.CN_Actualizar_Estados(_CE_Estados, out resultado, out mensaje);
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
