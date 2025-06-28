using API_ASISTENCIA_ESTUDIANTIL.ApiModels;
using CAPA_ENTIDADES;
using CAPA_NEGOCIO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_ASISTENCIA_ESTUDIANTIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Cls_AsignaturasController : ControllerBase
    {
        private readonly CN_ASIGNATURAS _CN_Asignaturas;

        int resultado;
        string mensaje;

        CE_ASIGNATURAS _CE_Asignaturas = new CE_ASIGNATURAS();

        public Cls_AsignaturasController(IConfiguration configuration)
        {
            _CN_Asignaturas = new CN_ASIGNATURAS(configuration);

        }

        [HttpGet("OBTENER_ASIGNATURAS")]

        public ActionResult<CE_ASIGNATURAS> ListarAsignaturas()
        {
            var resultado = _CN_Asignaturas.CN_Listar_Asignaturas();

            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }
        }

        [HttpGet("BUSCAR_ASIGNATURAS_POR_ID")]

        public ActionResult<CE_ASIGNATURAS> BuscarAsignaturasid([FromHeader] int? Id_Asignatura)
        {
            _CE_Asignaturas.Id_Asignatura = Id_Asignatura;

            var resultado = _CN_Asignaturas
                .CN_Filtrar_AsignaturasID(_CE_Asignaturas);

            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }
        }


        [HttpGet("BUSCAR_ASIGNATURAS")]

        public ActionResult<CE_ASIGNATURAS> BuscarAsignaturas([FromHeader] string? NombreAsignatura)
        {
            _CE_Asignaturas.Nombre_Asignatura = NombreAsignatura;

            var resultado = _CN_Asignaturas.CN_Filtrar_Asignaturas(_CE_Asignaturas);

            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }
        }

        [HttpPost("INSERTAR_ASIGNATURAS")]

        public ActionResult<List<CE_ASIGNATURAS>> Insertar_Asignaturas([FromBody] ApiModels.AM_ASIGNATURAS _am_Asignaturas)
        {
            _CE_Asignaturas.Id_Asignatura = _am_Asignaturas.Id_Asignatura;
            _CE_Asignaturas.Nombre_Asignatura = _am_Asignaturas.Nombre_Asignatura;
            _CE_Asignaturas.Id_Creador = _am_Asignaturas.Id_Creador;
            _CE_Asignaturas.Id_Estado = _am_Asignaturas.Id_Estado;

            try
            {
                _CN_Asignaturas.CN_Insertar_Asignaturas(_CE_Asignaturas);
                return Ok("ASIGNATURA INSERTADA CORRECTAMENTE");
            }
            catch (Exception ex)
            {
                string msg = ex.Message;

                return BadRequest(msg);
            }
        }

        [HttpPut("ACTUALIZAR_ASIGNATURAS")]

        public ActionResult<List<CE_ASIGNATURAS>> Actualizar_Asignaturas([FromBody] ApiModels.AM_ASIGNATURAS _am_Asignaturas)
        {

            _CE_Asignaturas.Id_Asignatura = _am_Asignaturas.Id_Asignatura;
            _CE_Asignaturas.Nombre_Asignatura = _am_Asignaturas.Nombre_Asignatura;
            _CE_Asignaturas.Id_Modificador = _am_Asignaturas.Id_Modificador;
            _CE_Asignaturas.Id_Estado = _am_Asignaturas.Id_Estado;

            AM_RESULTADO _RESULTADO = new AM_RESULTADO();
           

            try
            {
                _CN_Asignaturas.CN_Actualizar_Asignaturas(_CE_Asignaturas, out resultado, out mensaje);
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
