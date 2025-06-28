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
    public class CN_GRUPO_ASIGNATURA
    {
        private readonly CD_GRUPO_ASIGNATURA _cd_GA;

        public CN_GRUPO_ASIGNATURA(IConfiguration configuration)
        {
            _cd_GA = new CD_GRUPO_ASIGNATURA(configuration);
        }

        public void CN_Insertar_Grupo_Asignatura(CE_GRUPO_ASIGNATURA obj_GA)
        {
            _cd_GA.InsertarGrupoAsignatura(obj_GA);
        }

        public void CN_Actualizar_Grupo_Asignatura(CE_GRUPO_ASIGNATURA obj_GA, out int resultado, out string mensaje)
        {
            _cd_GA.ActualizarGrupoAsignatura(obj_GA, out resultado, out mensaje);
        }

        public List<CE_GRUPO_ASIGNATURA> CN_Listar_Grupo_Asignatura()
        {
           return _cd_GA.ListarGrupoAsignatura();
        }

        public List<CE_GRUPO_ASIGNATURA> CN_Filtrar_Grupo_Asignatura(CE_GRUPO_ASIGNATURA obj_GA)
        {
            return _cd_GA.FiltrarGrupoAsignatura(obj_GA);
        }
    public List<CE_GRUPO_ASIGNATURA> CN_Filtrar_Grupo_AsignaturaID(CE_GRUPO_ASIGNATURA obj_GA)
    {
        return _cd_GA.FiltrarGrupoAsignatura(obj_GA);
    }

}
}
