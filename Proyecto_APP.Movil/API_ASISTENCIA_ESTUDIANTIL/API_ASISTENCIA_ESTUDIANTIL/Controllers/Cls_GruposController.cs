using API_ASISTENCIA_ESTUDIANTIL.ApiModels;
using CAPA_ENTIDADES;
using CAPA_NEGOCIO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_ASISTENCIA_ESTUDIANTIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Cls_GruposController : ControllerBase
    {
        private readonly CN_GRUPOS _CN_Grupos;

        int resultado;
        string mensaje;

        CE_GRUPOS _CE_Grupos = new CE_GRUPOS();

        public Cls_GruposController(IConfiguration configuration)
        {
            _CN_Grupos = new CN_GRUPOS(configuration);
        }

        [HttpGet("OBTENER_GRUPOS")]
         public ActionResult<List<CE_GRUPOS>> Listar_Grupos()
        {
            var resultado = _CN_Grupos.CN_Listar_Grupos();

            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }
        }

        [HttpGet("BUSCAR_GRUPOS_POR_ID")]
        public ActionResult<List<CE_GRUPOS>> Buscar_GruposID([FromHeader] int? Id_Grupo)
        {

            _CE_Grupos.Id_Grupo = Id_Grupo;

            var resultado = _CN_Grupos.CN_Filtrar_GruposID(_CE_Grupos);


            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }

        }

        [HttpGet("BUSCAR_GRUPOS")]
        public ActionResult<List<CE_GRUPOS>> Buscar_Grupos([FromHeader] string? CodigoGrupo)
        {

            _CE_Grupos.Codigo_Grupo = CodigoGrupo;
            
            var resultado = _CN_Grupos.CN_Filtrar_Grupos(_CE_Grupos);


            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }

        }

        [HttpPost("INSERTAR_GRUPOS")]

        public ActionResult Insertar_Grupo([FromBody] ApiModels.AM_GRUPOS _Am_Grupos)
        {
            
            _CE_Grupos.Codigo_Grupo = _Am_Grupos.Codigo_Grupo;
            _CE_Grupos.Id_Creador = _Am_Grupos.Id_Creador;
            _CE_Grupos.Id_Estado = _Am_Grupos.Id_Estado;

            try
            {
                _CN_Grupos.CN_Insertar_Grupos(_CE_Grupos);

                return Ok("GRUPO INSERTADO CORRECTAMENTE");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("ACTUALIZAR_GRUPOS")]

        public ActionResult Actualizar_Grupo([FromBody] ApiModels.AM_GRUPOS _Am_Grupos)
        {
            _CE_Grupos.Id_Grupo = _Am_Grupos.Id_Grupo;
            _CE_Grupos.Codigo_Grupo = _Am_Grupos.Codigo_Grupo;
            _CE_Grupos.Id_Modificador = _Am_Grupos.Id_Modificador;
            _CE_Grupos.Id_Estado = _Am_Grupos.Id_Estado;

            AM_RESULTADO _RESULTADO =  new AM_RESULTADO();

            try
            {
                _CN_Grupos.CN_Actualizar_Grupos(_CE_Grupos, out resultado, out mensaje);
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
