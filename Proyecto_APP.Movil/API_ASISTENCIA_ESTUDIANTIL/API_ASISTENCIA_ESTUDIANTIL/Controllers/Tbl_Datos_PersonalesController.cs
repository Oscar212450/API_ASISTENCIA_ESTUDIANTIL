using API_ASISTENCIA_ESTUDIANTIL.ApiModels;
using CAPA_ENTIDADES;
using CAPA_NEGOCIO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_ASISTENCIA_ESTUDIANTIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Tbl_Datos_PersonalesController : ControllerBase
    {
        private readonly CN_DATOS_PERSONALES _CNDatos_Personales;

        int resultado;
        string mensaje;


        CE_DATOS_PERSONALES _CEDatos_Personales = new CE_DATOS_PERSONALES();
        public Tbl_Datos_PersonalesController(IConfiguration configuration)
        {
            _CNDatos_Personales = new CN_DATOS_PERSONALES(configuration);
            
        }

        [HttpGet("OBTENER_DATOS_PERSONALES")]
        public ActionResult<List<CE_CATALOGOS>> ListarCatalogos()
        {
            var resultado = _CNDatos_Personales.CN_Listar_Datos_Personales();
            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }

        }
        [HttpGet("BUSCAR_DATOS_PERSONALES_POR_ID")]
        public ActionResult<List<CE_DATOS_PERSONALES>> FiltrarDatosPersonalesID([FromHeader] int? Id_Persona)
        {

            
            _CEDatos_Personales.Id_Persona = Id_Persona;

            var resultado = _CNDatos_Personales.CN_Filtrar_Datos_PersonalesID(_CEDatos_Personales);
            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }

        }

        [HttpGet("BUSCAR_DATOS_PERSONALES")]
        public ActionResult<List<CE_DATOS_PERSONALES>> FiltrarDatosPersonales([FromHeader] string? DNI, string? Apellido1)
        {

            _CEDatos_Personales.DNI = DNI;
            _CEDatos_Personales.Primer_Apellido = Apellido1;

            var resultado = _CNDatos_Personales.CN_Filtrar_Datos_Personales(_CEDatos_Personales);
            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }

        }
        [HttpPost("INSERTAR_DATOS_PERSONALES")]
        public ActionResult InsertarDatosPersonales([FromBody] ApiModels.AM_DATOS_PERSONALES _Am_Datos_Personales)
        {
           
            _CEDatos_Personales.Primer_Nombre = _Am_Datos_Personales.Primer_Nombre;
            _CEDatos_Personales.Segundo_Nombre = _Am_Datos_Personales.Segundo_Nombre;
            _CEDatos_Personales.Primer_Apellido = _Am_Datos_Personales.Primer_Apellido;
            _CEDatos_Personales.Segundo_Apellido = _Am_Datos_Personales.Segundo_Apellido;
            _CEDatos_Personales.Edad = _Am_Datos_Personales.Edad;
            _CEDatos_Personales.Tipo_Cargo = _Am_Datos_Personales.Tipo_Cargo;
            _CEDatos_Personales.Tipo_DNI = _Am_Datos_Personales.Tipo_DNI;
            _CEDatos_Personales.DNI = _Am_Datos_Personales.DNI;
            _CEDatos_Personales.Genero = _Am_Datos_Personales.Genero;
            _CEDatos_Personales.Nacionalidad = _Am_Datos_Personales.Nacionalidad;
            _CEDatos_Personales.Departamento = _Am_Datos_Personales.Departamento;
            _CEDatos_Personales.Estado_Civil = _Am_Datos_Personales.Estado_Civil;
            _CEDatos_Personales.Id_Creador = _Am_Datos_Personales.Id_Creador;
            _CEDatos_Personales.Id_Estado = _Am_Datos_Personales.Id_Estado;


            try
            {
                _CNDatos_Personales.CN_Insertar_Datos_Personales(_CEDatos_Personales);
                return Ok("DATOS PERSONALES INSERTADOS CORRECTAMENTE");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpPut("ACTUALIZAR_DATOS_PERSONALES")]
        public ActionResult ActualizarDatosPersonales([FromBody] ApiModels.AM_DATOS_PERSONALES _Am_Datos_Personales)
        {
            _CEDatos_Personales.Id_Persona = _Am_Datos_Personales.Id_Persona;
            _CEDatos_Personales.Primer_Nombre = _Am_Datos_Personales.Primer_Nombre;
            _CEDatos_Personales.Segundo_Nombre = _Am_Datos_Personales.Segundo_Nombre;
            _CEDatos_Personales.Primer_Apellido = _Am_Datos_Personales.Primer_Apellido;
            _CEDatos_Personales.Segundo_Apellido = _Am_Datos_Personales.Segundo_Apellido;
            _CEDatos_Personales.Edad = _Am_Datos_Personales.Edad;
            _CEDatos_Personales.Tipo_Cargo = _Am_Datos_Personales.Tipo_Cargo;
            _CEDatos_Personales.Tipo_DNI = _Am_Datos_Personales.Tipo_DNI;
            _CEDatos_Personales.DNI = _Am_Datos_Personales.DNI;
            _CEDatos_Personales.Genero = _Am_Datos_Personales.Genero;
            _CEDatos_Personales.Nacionalidad = _Am_Datos_Personales.Nacionalidad;
            _CEDatos_Personales.Departamento = _Am_Datos_Personales.Departamento;
            _CEDatos_Personales.Estado_Civil = _Am_Datos_Personales.Estado_Civil;
            _CEDatos_Personales.Id_Modificador = _Am_Datos_Personales.Id_Modificador;
            _CEDatos_Personales.Id_Estado = _Am_Datos_Personales.Id_Estado;

            AM_RESULTADO _resultado = new AM_RESULTADO();

            try
            {
                _CNDatos_Personales.CN_Actualizar_Datos_Personales(_CEDatos_Personales, out resultado, out mensaje);
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
