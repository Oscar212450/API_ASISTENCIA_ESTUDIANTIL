using CAPA_DATOS;
using CAPA_ENTIDADES;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIO
{
    public class CN_GRUPO_TURNO
    {
        private readonly CD_GRUPO_TURNO _cd_GT;

        public CN_GRUPO_TURNO(IConfiguration configuration)
        {
            _cd_GT = new CD_GRUPO_TURNO(configuration);
        }

        public void CN_Insertar_Grupo_Turno(CE_GRUPO_TURNO obj_GT)
        {
            _cd_GT.InsertarGrupoTurno(obj_GT);
        }

        public void CN_Actualizar_Grupo_Turno(CE_GRUPO_TURNO obj_GT, out int resultado, out string mensaje)
        {
            _cd_GT.ActualizarGrupoTurno(obj_GT, out resultado, out mensaje);
        }

        public List<CE_GRUPO_TURNO> CN_Listar_Grupo_Turno()
        {
           return _cd_GT.ListarGrupoTurno();
        }

        public List<CE_GRUPO_TURNO> CN_Filtrar_Grupo_Turno(CE_GRUPO_TURNO obj_GT)
        {
            return _cd_GT.FiltrarGrupoTurno(obj_GT);
        }

        public List<CE_GRUPO_TURNO> CN_Filtrar_Grupo_TurnoID(CE_GRUPO_TURNO obj_GT)
        {
            return _cd_GT.FiltrarGrupoTurno(obj_GT);
        }
    }
}
