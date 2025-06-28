using API_ASISTENCIA_ESTUDIANTIL.ApiModels;
using CAPA_ENTIDADES;
using CAPA_NEGOCIO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace API_ASISTENCIA_ESTUDIANTIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Cls_CatalogosController : ControllerBase
    {
        private readonly CN_CATALOGOS _CNCatalogos;

        int resultado;
        string mensaje;

        CE_CATALOGOS _CECatalogos = new CE_CATALOGOS();

        public Cls_CatalogosController(IConfiguration configuration)

        {
            _CNCatalogos = new CN_CATALOGOS(configuration);
       
            
        }

        [HttpGet("OBTENER_CATALOGOS")]
        public ActionResult<List<CE_CATALOGOS>> ListarCatalogos()
        {
            var resultado = _CNCatalogos.CN_Listar_Catalogos();
            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }

        }

        [HttpGet("BUSCAR_CATALOGOS_POR_ID")]
        public ActionResult<List<CE_CATALOGOS>> FiltrarCatalogos_id([FromHeader] int? ID_Catalogo)
        {

            _CECatalogos.Id_Catalogo = ID_Catalogo;

            var resultado = _CNCatalogos.CN_Filtrar_CatalogosID(_CECatalogos);
            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }

        }

        [HttpGet("BUSCAR_CATALOGOS")]
        public ActionResult<List<CE_CATALOGOS>> FiltrarCatalogos([FromHeader] string? Catalogo)
        {

            _CECatalogos.Catalogo = Catalogo;

            var resultado = _CNCatalogos.CN_Filtrar_Catalogos(_CECatalogos);
            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }

        }
        [HttpPost("INSERTAR_CATALOGO")]
        public ActionResult InsertarCatalogo([FromBody] ApiModels.AM_CATALOGOS _Am_Catalogos)
        {
            _CECatalogos.Id_Tipo_Catalogo = _Am_Catalogos.Id_Tipo_Catalogo;
            _CECatalogos.Catalogo = _Am_Catalogos.Catalogo;
            _CECatalogos.Id_Creador = _Am_Catalogos.Id_Creador;
            _CECatalogos.Activo = _Am_Catalogos.Activo;

            try
            {
                _CNCatalogos.CN_Insertar_Catalogos(_CECatalogos);
                return Ok("CATALOGO INSERTADO CORRECTAMENTE");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpPut("ACTUALIZAR_CATALOGO")]
        public ActionResult ActualizarCatalogo([FromBody] ApiModels.AM_CATALOGOS _Am_Catalogos)
        {
            _CECatalogos.Id_Catalogo = _Am_Catalogos.Id_Catalogo;
            _CECatalogos.Id_Tipo_Catalogo = _Am_Catalogos.Id_Tipo_Catalogo;
            _CECatalogos.Catalogo = _Am_Catalogos.Catalogo;
            _CECatalogos.Id_Modificador = _Am_Catalogos.Id_Modificador;
            _CECatalogos.Activo = _Am_Catalogos.Activo;

            AM_RESULTADO _resultado = new AM_RESULTADO();

            try
            {
                _CNCatalogos.CN_Actualizar_Catalogos(_CECatalogos, out resultado, out mensaje);
                _resultado.resultado = resultado;
                _resultado.mensaje = mensaje;
                return Ok(_resultado);
            }
            catch 
            {
                return BadRequest(_resultado);
            }


        }
    }
}

