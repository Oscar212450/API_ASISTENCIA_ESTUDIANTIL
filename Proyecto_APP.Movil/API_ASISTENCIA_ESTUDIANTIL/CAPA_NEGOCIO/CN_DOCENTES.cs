using CAPA_DATOS;
using CAPA_ENTIDADES;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIO
{
    public class CN_DOCENTES
    {
        private readonly CD_DOCENTES _CD_Docentes;

        public CN_DOCENTES(IConfiguration configuration)
        {
            _CD_Docentes = new CD_DOCENTES(configuration);
        }

        public void CN_Insertar_Docente(CE_DOCENTES obj_docentes)
        {
            _CD_Docentes.InsertarDocentes(obj_docentes);
        }

        public void CN_Actualizar_Docente(CE_DOCENTES obj_docentes, out int resultado, out string mensaje)
        {
            _CD_Docentes.ActualizarDocentes(obj_docentes, out resultado, out mensaje);
        }

        public List<CE_DOCENTES> CN_Listar_Docentes()
        {
            return _CD_Docentes.ListarDocentes();
        }
        public List<CE_DOCENTES> CN_Filtrar_Docentes(CE_DOCENTES obj_docentes)
        {
            return _CD_Docentes.FiltrarDocentes(obj_docentes);
        }

        public List<CE_DOCENTES> CN_Filtrar_DocentesID(CE_DOCENTES obj_docentes)
        {
            return _CD_Docentes.FiltrarDocentesid(obj_docentes);
        }
    }
}
