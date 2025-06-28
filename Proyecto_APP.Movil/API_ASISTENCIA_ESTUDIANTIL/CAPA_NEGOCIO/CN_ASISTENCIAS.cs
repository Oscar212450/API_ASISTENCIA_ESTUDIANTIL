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
    public class CN_ASISTENCIAS
    {
        private readonly CD_ASISTENCIAS _CD_Asistencias;

        public CN_ASISTENCIAS(IConfiguration configuration)
        {
            _CD_Asistencias = new CD_ASISTENCIAS (configuration);
        }

        public void CN_Insertar_Asistencia(CE_ASISTENCIAS _CE_Asistencias)
        {
            _CD_Asistencias.InsertarAsistencia(_CE_Asistencias);
        }

        public void CN_Actualizar_Asistencia(CE_ASISTENCIAS _CE_Asistencias, out int resultado, out string mensaje)
        {
            _CD_Asistencias.ActualizarAsistencia(_CE_Asistencias,out resultado,out mensaje);
        }

        public List<CE_ASISTENCIAS> CN_Filtrar_Asistencias(CE_ASISTENCIAS _CE_Asistencias)
        {
           return _CD_Asistencias.FiltrarAsistenciaID(_CE_Asistencias);
        }

        public List<CE_ASISTENCIAS> CN_Listar_Asistencias()
        {
            return _CD_Asistencias.ListardAsistencias();
        }
        public List<CE_ASISTENCIAS> CN_Filtrar_AsistenciasID(CE_ASISTENCIAS _CE_Asistencias)
        {
            return _CD_Asistencias.FiltrarAsistenciaID(_CE_Asistencias);
        }
    }
}
