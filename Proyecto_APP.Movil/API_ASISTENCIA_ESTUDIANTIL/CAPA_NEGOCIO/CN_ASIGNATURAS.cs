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
    public class CN_ASIGNATURAS
    {
        private readonly CD_ASIGNATURAS _CD_Asignaturas;

        public CN_ASIGNATURAS(IConfiguration configuration)
        {

            _CD_Asignaturas = new CD_ASIGNATURAS(configuration);
        }

        public void CN_Insertar_Asignaturas(CE_ASIGNATURAS obj_asignaturas)
        {
            _CD_Asignaturas.InsertarAsignaturas(obj_asignaturas);
        }

        public void CN_Actualizar_Asignaturas(CE_ASIGNATURAS obj_asignaturas, out int resultado, out string mensaje)
        {
            _CD_Asignaturas.ActualizarAsignaturas(obj_asignaturas, out resultado, out mensaje);
        }

        public List<CE_ASIGNATURAS> CN_Listar_Asignaturas()
        {
            return _CD_Asignaturas.ListarAsignaturas();
        }

        public List<CE_ASIGNATURAS> CN_Filtrar_Asignaturas(CE_ASIGNATURAS obj_asignaturas)
        {
            return _CD_Asignaturas.FiltrarAsignaturas(obj_asignaturas);
        }

        public List<CE_ASIGNATURAS> CN_Filtrar_AsignaturasID(CE_ASIGNATURAS obj_asignaturas)
        {
            return _CD_Asignaturas.FiltrarAsignaturasID(obj_asignaturas);
        }

    }
}
