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
    public class CN_ESTADOS
    {
        private readonly CD_ESTADOS _CD_ESTADOS;

        public CN_ESTADOS(IConfiguration configuration)
        {
            _CD_ESTADOS = new CD_ESTADOS(configuration);
        }

        public void CN_Insertar_Estados(CE_ESTADOS _CE_ESTADOS)
        {
            _CD_ESTADOS.InsertarEstados(_CE_ESTADOS);
        }

        public void CN_Actualizar_Estados(CE_ESTADOS _CE_ESTADOS, out int resultado, out string mensaje)
        {
            _CD_ESTADOS.ActualizarEstado(_CE_ESTADOS, out resultado, out mensaje);
        }

        public List<CE_ESTADOS> CN_Listar_Estados()
        {
           return _CD_ESTADOS.Listar_Estado();
        }

        public List<CE_ESTADOS> CN_Filtrar_Estado(CE_ESTADOS obj)
        {
            return _CD_ESTADOS.Filtrar_Estado(obj);
        }
        public List<CE_ESTADOS> CN_Filtrar_EstadoID(CE_ESTADOS obj)
        {
            return _CD_ESTADOS.CD_Filtrar_EstadoID(obj)
                ;
        }
    }
}
