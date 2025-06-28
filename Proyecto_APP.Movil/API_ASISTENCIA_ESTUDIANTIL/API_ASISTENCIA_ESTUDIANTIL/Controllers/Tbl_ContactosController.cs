using API_ASISTENCIA_ESTUDIANTIL.ApiModels;
using CAPA_ENTIDADES;
using CAPA_NEGOCIO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_ASISTENCIA_ESTUDIANTIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Tbl_ContactosController : ControllerBase
    {
        private readonly CN_CONTACTOS _CN_Contactos;
       

        CE_CONTACTOS _CE_Contactos = new CE_CONTACTOS();

        int resultado;
        string mensaje;

        public Tbl_ContactosController(IConfiguration configuration)
        {
            _CN_Contactos = new CN_CONTACTOS(configuration);
           
        }

        [HttpGet("OBTENER_CONTACTOS")]
        public ActionResult<List<CE_CONTACTOS>> ListarContactos()
        {
            var resultado = _CN_Contactos.CN_Obtener_Contactos();
            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }

        }

        [HttpGet("BUSCAR_CONTACTOS_POR_ID")]
        public ActionResult<List<CE_CONTACTOS>> FiltrarContactosId([FromHeader] int? Id_Contacto)
        {

            _CE_Contactos.Id_Contacto = Id_Contacto;


            var resultado = _CN_Contactos.CN_Filtrar_ContactosID(_CE_Contactos);
            
            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }

        }

        [HttpGet("BUSCAR_CONTACTOS")]
        public ActionResult<List<CE_CONTACTOS>> FiltrarContactos([FromHeader] string? Contacto)
        {

            _CE_Contactos.Contacto = Contacto;


            var resultado = _CN_Contactos.CN_Filtrar_Contactos(_CE_Contactos);
            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }

        }

        [HttpPost("INSERTAR_CONTACTOS")]
        public ActionResult InsertarContactos([FromBody] ApiModels.AM_CONTACTOS _Am_Contactos)
        {
            _CE_Contactos.Id_Persona = _Am_Contactos.Id_Persona;
            _CE_Contactos.Tipo_Contacto = _Am_Contactos.Tipo_Contacto;
            _CE_Contactos.Contacto = _Am_Contactos.Contacto;
            _CE_Contactos.Codigo_Postal = _Am_Contactos.Codigo_Postal;
            _CE_Contactos.Pais = _Am_Contactos.Pais;
            _CE_Contactos.Id_Creador = _Am_Contactos.Id_Creador;
            _CE_Contactos.Id_Estado = _Am_Contactos.Id_Estado;

            try
            {
                _CN_Contactos.CN_Insertar_Contactos(_CE_Contactos);
                return Ok("CONTACTO INSERTADO CORRECTAMENTE");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("ACTUALIZAR_CONTACTOS")]
        public ActionResult ActualizarContactos([FromBody] ApiModels.AM_CONTACTOS _Am_Contactos)
        {
            _CE_Contactos.Id_Contacto = _Am_Contactos.Id_Contacto;
            _CE_Contactos.Id_Persona = _Am_Contactos.Id_Persona;
            _CE_Contactos.Tipo_Contacto = _Am_Contactos.Tipo_Contacto;
            _CE_Contactos.Contacto = _Am_Contactos.Contacto;
            _CE_Contactos.Codigo_Postal = _Am_Contactos.Codigo_Postal;
            _CE_Contactos.Pais = _Am_Contactos.Pais;
            _CE_Contactos.Id_Modificador = _Am_Contactos.Id_Modificador;
            _CE_Contactos.Id_Estado = _Am_Contactos.Id_Estado;

            AM_RESULTADO _resutado = new AM_RESULTADO();

            try
            {
                _CN_Contactos.CN_Actualizar_Contactos(_CE_Contactos, out resultado, out mensaje);
                _resutado.resultado = resultado;
                _resutado.mensaje = mensaje;
                return Ok(_resutado);
            }
            catch 
            {
                return BadRequest(_resutado);
            }
        }
    }
}

