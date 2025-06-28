using Capa_Dato;
using Capa_Entidades;
using Capa_Negocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Api_movil.Controllers.API_model;

namespace Proyecto_Api_movil.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Autor_Controller : ControllerBase
    {
        private readonly CN_Autor _CNautor;
        private readonly CE_Autor _CE_Autor = new CE_Autor();

        public Autor_Controller(IConfiguration configuration)
        {
            _CNautor = new CN_Autor(configuration);
        }

        // GET: Listar todos los autores
        [HttpGet("Get_LISTAR_AUTORES")]
        public ActionResult<List<CE_Autor>> LISTAR_AUTORES()
        {
            try
            {
                var resultado = _CNautor.Listar_Autores();
                if (resultado.Count == 0)
                    return NoContent();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Filtrar autor por nombre
        [HttpGet("Get_FILTRAR_AUTOR_NOMBRE")]
        public ActionResult<List<CE_Autor>> Filtrar_Nombre([FromHeader] string? nombre)
        {
            _CE_Autor.Nombre = nombre;
            var resultado = _CNautor.Filtrar_Autor(_CE_Autor);

            if (resultado.Count == 0)
                return NoContent();
            return Ok(resultado);
        }

        // GET: Filtrar autor por ID
        [HttpGet("Get_FILTRAR_AUTOR_ID")]
        public ActionResult<CE_Autor> Filtrar_por_ID([FromHeader] int id_autor)
        {
            var resultado = _CNautor.Filtrar_Autor_Por_ID(id_autor);

            if (resultado == null)
                return NoContent();
            return Ok(resultado);
        }


        // POST: Insertar nuevo autor
        [HttpPost("Post_INSERTAR_AUTOR")]
        public ActionResult INSERTAR_AUTOR([FromBody] API_model.AM_Autor am_autor)
        {
            _CE_Autor.Id_autor = am_autor.Id_autor;
            _CE_Autor.Nombre = am_autor.Nombre;
            _CE_Autor.Biografia = am_autor.Biografia;
            _CE_Autor.Sitio_web = am_autor.Sitio_web;

            try
            {
                _CNautor._INSERTAR_Autor(_CE_Autor);
                return Ok("Autor insertado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: Actualizar autor existente
        [HttpPut("Put_ACTUALIZAR_AUTOR")]
        public ActionResult ACTUALIZAR_AUTOR([FromBody] API_model.AM_Autor am_autor)
        {
            _CE_Autor.Id_autor = am_autor.Id_autor;
            _CE_Autor.Nombre = am_autor.Nombre;
            _CE_Autor.Biografia = am_autor.Biografia;
            _CE_Autor.Sitio_web = am_autor.Sitio_web;

            try
            {
                _CNautor._Actualizar_Autor(_CE_Autor);
                return Ok("Autor actualizado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
