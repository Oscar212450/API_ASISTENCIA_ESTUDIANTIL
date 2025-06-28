using CAPA_DATOS;
using CAPA_ENTIDADES;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIO
{
    public class CN_DOCENTE_GRUPO
    {
        private readonly CD_DOCENTE_GRUPO _CD_DG;

        public CN_DOCENTE_GRUPO(IConfiguration configuration)
        {
            _CD_DG = new CD_DOCENTE_GRUPO(configuration);
        }

        public void CN_Insertar_Docente_Grupo(CE_DOCENTE_GRUPO _CE_DG)
        {
            _CD_DG.InsertarDocenteGrupo(_CE_DG);
        }

        public void CN_Actualizar_Docente_Grupo(CE_DOCENTE_GRUPO _CE_DG, out int resultado, out string mensaje)
        {
            _CD_DG.ActualizarDocenteGrupo(_CE_DG, out resultado, out mensaje);
        }

        public List<CE_DOCENTE_GRUPO> CN_Filtrar_Docente_Grupo(CE_DOCENTE_GRUPO _CE_DG)
        {
           return _CD_DG.FiltrarDocenteGrupo(_CE_DG);
        }

        public List<CE_DOCENTE_GRUPO> CN_Listar_Docente_Grupo()
        {
            return _CD_DG.ListardDocenteGrupo();

        }
        public List<CE_DOCENTE_GRUPO> CN_Filtrar_Docente_GrupoID(CE_DOCENTE_GRUPO _CE_DG)
        {
            return _CD_DG.FiltrarDocenteGrupoID(_CE_DG);
        }

    }
}
