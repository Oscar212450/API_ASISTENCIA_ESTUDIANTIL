using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAPA_DATOS;
using CAPA_ENTIDADES;
using Microsoft.Extensions.Configuration;

namespace CAPA_NEGOCIO
{
    public class CN_HORARIOS
    {
        private readonly CD_HORARIOS _CDHorarios;
        public CN_HORARIOS(IConfiguration configuration)
        {
            _CDHorarios = new CD_HORARIOS(configuration);
        }

        public void CN_Insertar_Horarios(CE_HORARIOS obj_Horarios)
        {
            _CDHorarios.InsertarHorario(obj_Horarios);

        }
        public void CN_Actualizar_Horarios(CE_HORARIOS obj_Horarios, out int resultado, out string mensaje)
        {
            _CDHorarios.ActualizarHorario(obj_Horarios, out resultado, out mensaje);

        }
       public List<CE_HORARIOS> CN_Filtrar_Horarios(CE_HORARIOS obj_Horarios)
        {
            return _CDHorarios.FiltrarHorarios(obj_Horarios);
        }
        public List<CE_HORARIOS> CN_Listar_Horarios()
        {
            return _CDHorarios.ListarHorarios();
        }

        public List<CE_HORARIOS> CN_Filtrar_HorariosID(CE_HORARIOS obj_Horarios)
        {
            return _CDHorarios.FiltrarHorarios(obj_Horarios);
        }
    }
}
