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
    public class CN_GRUPOS
    {
        private readonly CD_GRUPOS _CD_Grupos;

        public CN_GRUPOS(IConfiguration configuration)
        {
            _CD_Grupos = new CD_GRUPOS(configuration);
        }

        public void CN_Insertar_Grupos(CE_GRUPOS _CE_Grupos)
        {
            _CD_Grupos.InsertarGrupo(_CE_Grupos);
        }

        public void CN_Actualizar_Grupos(CE_GRUPOS _CE_Grupos, out int resultado, out string mensaje)
        {
            _CD_Grupos.ActualizarGrupo(_CE_Grupos,out resultado, out mensaje);
        }

        public List<CE_GRUPOS> CN_Filtrar_Grupos(CE_GRUPOS _CE_Grupos)
        {
           return _CD_Grupos.FiltrarGrupos(_CE_Grupos);
        }

        public List<CE_GRUPOS> CN_Listar_Grupos()
        {
            return _CD_Grupos.ListarGrupos();
        }

        public List<CE_GRUPOS> CN_Filtrar_GruposID(CE_GRUPOS _CE_Grupos)
        {
            return _CD_Grupos.FiltrarGruposID(_CE_Grupos);
        }
    }
}
