using API_ASISTENCIA_ESTUDIANTIL.ApiModels;
using CAPA_ENTIDADES;
using CAPA_NEGOCIO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_ASISTENCIA_ESTUDIANTIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Tbl_AsistenciasController : ControllerBase
    {
        private readonly CN_ASISTENCIAS _CN_Asistencias;

        int resultado;
        string mensaje;

        CE_ASISTENCIAS _CE_Asistencias = new CE_ASISTENCIAS();

        public Tbl_AsistenciasController(IConfiguration configuration)
        {
            _CN_Asistencias = new CN_ASISTENCIAS (configuration);  
           
        }

        [HttpGet("OBTENER_ASISTENCIAS")]

        public ActionResult<List<CE_ASISTENCIAS>> Listar_Asistencias()
        {
            var resultado = _CN_Asistencias.CN_Listar_Asistencias();

            if(resultado.Count == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }
        }

        [HttpGet("BUSCAR_ASISTENCIAS_POR_ID")]

        public ActionResult<List<CE_ASISTENCIAS>> FiltrarAsistencias([FromHeader] int? Id_Asistencia)
                                                                     
        {
            _CE_Asistencias.Id_Asistencia = Id_Asistencia;

            var resultado = _CN_Asistencias.CN_Filtrar_AsistenciasID(_CE_Asistencias);

            if (resultado.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }
        }

        [HttpGet("BUSCAR_ASISTENCIAS")]

        public ActionResult<List<CE_ASISTENCIAS>> FiltrarAsistencias([FromHeader] int? Id_Estudiante_Grupo, 
                                                                    int? Id_Grupo_Asignatura, string? Fecha, string? Asistio)
        {
            _CE_Asistencias.Id_Estudiante_Grupo = Id_Estudiante_Grupo;
            _CE_Asistencias.Id_Grupo_Asignatura = Id_Grupo_Asignatura;
            _CE_Asistencias.Fecha = Fecha;
            _CE_Asistencias.Asistio = Asistio;

            var resultado = _CN_Asistencias.CN_Filtrar_Asistencias(_CE_Asistencias);

            if (resultado.Count == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(resultado);
            }
        }

        [HttpPost("INSERTAR_ASISTENCIA")]

        public ActionResult InsertarDocenteGrupo([FromBody] ApiModels.AM_ASISTENCIAS _am_Asistencias)
        {
            _CE_Asistencias.Id_Estudiante_Grupo = _am_Asistencias.Id_Estudiante_Grupo;
            _CE_Asistencias.Id_Grupo_Asignatura = _am_Asistencias.Id_Grupo_Asignatura;
            _CE_Asistencias.Id_Docente_Grupo = _am_Asistencias.Id_Docente_Grupo;
            _CE_Asistencias.Fecha = _am_Asistencias.Fecha;
            _CE_Asistencias.Asistio = _am_Asistencias.Asistio;
            _CE_Asistencias.Observacion = _am_Asistencias.Observacion;
            _CE_Asistencias.Id_Creador = _am_Asistencias.Id_Creador;
            _CE_Asistencias.Id_Estado = _am_Asistencias.Id_Estado;


            try
            {
                _CN_Asistencias.CN_Insertar_Asistencia(_CE_Asistencias);

                return Ok("REGISTRO_DE_ASISTENCIA_INSERTADO_CORRECTAMENTE");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("ACTUALIZAR_ASISTENCIA")]

        public ActionResult ActualizarAsistencia([FromBody] ApiModels.AM_ASISTENCIAS _am_Asistencias)
        {
            _CE_Asistencias.Id_Asistencia = _am_Asistencias.Id_Asistencia;
            _CE_Asistencias.Id_Estudiante_Grupo = _am_Asistencias.Id_Estudiante_Grupo;
            _CE_Asistencias.Id_Grupo_Asignatura = _am_Asistencias.Id_Grupo_Asignatura;
            _CE_Asistencias.Id_Docente_Grupo = _am_Asistencias.Id_Docente_Grupo;
            _CE_Asistencias.Fecha = _am_Asistencias.Fecha;
            _CE_Asistencias.Asistio = _am_Asistencias.Asistio;
            _CE_Asistencias.Observacion = _am_Asistencias.Observacion;
            _CE_Asistencias.Id_Creador = _am_Asistencias.Id_Creador;
            _CE_Asistencias.Id_Estado = _am_Asistencias.Id_Estado;

            AM_RESULTADO _RESULTADO = new AM_RESULTADO();

            try
            {
                _CN_Asistencias.CN_Actualizar_Asistencia(_CE_Asistencias, out resultado, out mensaje);
                _RESULTADO.resultado = resultado;
                _RESULTADO.mensaje = mensaje;
                return Ok(_RESULTADO);
            }
            catch (Exception ex)
            {
                return BadRequest(_RESULTADO);
            }

        }
    }
}
