using CAPA_NEGOCIO;
using CAPA_ENTIDADES;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using API_ASISTENCIA_ESTUDIANTIL.ApiModels;

namespace API_ASISTENCIA_ESTUDIANTIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Cls_Tipo_CatalogosController : ControllerBase
    {
        private readonly CN_TIPO_CATALOGOS _CN_TIpo_Catalogos;

        int resultado;
        string mensaje;

        CE_TIPO_CATALOGOS _CETipo_Catalogos = new CE_TIPO_CATALOGOS();

        public Cls_Tipo_CatalogosController(IConfiguration configuration)
        {
            _CN_TIpo_Catalogos = new CN_TIPO_CATALOGOS(configuration);
        }

        [HttpGet ("OBTENER_TIPO_CATALOGOS")]

        public ActionResult<List<CE_TIPO_CATALOGOS>> ListarTipoCatalogos()
        {
            var resultado = _CN_TIpo_Catalogos.CN_Listar_Tipo_Catalogos();

            if(resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }
            

        }

        [HttpGet("BUSCAR_TIPO_CATALOGO_POR_ID")]

        public ActionResult<List<CE_TIPO_CATALOGOS>> FiltrarTIpoCatalogoID([FromHeader] int? Id_Tipo_Catalogo)
        {


            _CETipo_Catalogos.Id_Tipo_Catalogo = Id_Tipo_Catalogo;

            var resultado = _CN_TIpo_Catalogos.CN_Filtrar_Tipo_CatalogosID(_CETipo_Catalogos);



            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }


        }

        [HttpGet("BUSCAR_TIPO_CATALOGO")]

        public ActionResult<List<CE_TIPO_CATALOGOS>> FiltrarTIpoCatalogo([FromHeader] string Tipos)
        {

          
            _CETipo_Catalogos.Tipo_Catalogo = Tipos;

            var resultado = _CN_TIpo_Catalogos.CN_Filtrar_Tipo_Catalogos(_CETipo_Catalogos);

           

            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }


        }

        [HttpPost("INSERTAR_TIPO_CATALOGO")]
        public ActionResult InsertarTipoCatalogo([FromBody] ApiModels.AM_TIPO_CATALOGOS _Am_Tipo_Catalogos)
        {
            _CETipo_Catalogos.Tipo_Catalogo = _Am_Tipo_Catalogos.Tipo_Catalogo;
            _CETipo_Catalogos.Id_Creador = _Am_Tipo_Catalogos.Id_Creador;
            _CETipo_Catalogos.Activo = _Am_Tipo_Catalogos.Activo;

            try
            {
                _CN_TIpo_Catalogos.CN_Insertar_Tipo_Catalogos( _CETipo_Catalogos );
                return Ok("TIPO DE CATALOGO INSERTADO CORRECTAMENTE");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpPut("ACTUALIZAR_TIPO_CATALOGO")]
        public ActionResult ActualizarTipoCatalogo([FromBody] ApiModels.AM_TIPO_CATALOGOS _Am_Tipo_Catalogos)
        {
            _CETipo_Catalogos.Id_Tipo_Catalogo = _Am_Tipo_Catalogos.Id_Tipo_Catalogo;
            _CETipo_Catalogos.Tipo_Catalogo = _Am_Tipo_Catalogos.Tipo_Catalogo;
            _CETipo_Catalogos.Id_Modificador = _Am_Tipo_Catalogos.Id_Modificador;
            _CETipo_Catalogos.Activo = _Am_Tipo_Catalogos.Activo;

            AM_RESULTADO _RESULTADO = new AM_RESULTADO();

            try
            {
                _CN_TIpo_Catalogos.CN_Actualizar_Tipo_Catalogos(_CETipo_Catalogos, out resultado, out mensaje);
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
