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
    public class CN_ESTUDIANTE_GRUPO
    {
        private readonly CD_ESTUDIANTE_GRUPO _CD_EG;

        public CN_ESTUDIANTE_GRUPO(IConfiguration configuration)
        {
            _CD_EG = new CD_ESTUDIANTE_GRUPO(configuration);
        }

        public void CN_Insertar_Estudiante_Grupo(CE_ESTUDIANTE_GRUPO _CE_EG, out int resultado, out string mensaje)
        {
            _CD_EG.InsertarEstudianteGrupo(_CE_EG, out resultado, out mensaje);
        }

        public void CN_Actualizar_Estudiante_Grupo(CE_ESTUDIANTE_GRUPO _CE_EG, out int resultado, out string mensaje)
        {
            _CD_EG.ActualizarEstudianteGrupo(_CE_EG, out resultado, out mensaje);
        }

        public List<CE_ESTUDIANTE_GRUPO> CN_Filtrar_Estudiante_Grupo(CE_ESTUDIANTE_GRUPO _CE_EG)
        {
           return _CD_EG.FiltrarEstudianteGrupo(_CE_EG);
        }

        public List<CE_ESTUDIANTE_GRUPO> CN_Listar_Estudiante_Grupo()
        {
            return _CD_EG.ListardEstudianteGrupo();
        }

        public List<CE_ESTUDIANTE_GRUPO> CN_Filtrar_Estudiante_GrupoID(CE_ESTUDIANTE_GRUPO _CE_EG)
        {
            return _CD_EG.FiltrarEstudianteGrupo(_CE_EG);
        }
    }
}
