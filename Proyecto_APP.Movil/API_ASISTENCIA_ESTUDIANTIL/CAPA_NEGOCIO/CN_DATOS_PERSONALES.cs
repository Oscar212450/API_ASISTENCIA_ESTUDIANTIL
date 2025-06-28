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
    public class CN_DATOS_PERSONALES
    {
        private readonly CD_DATOS_PERSONALES _CDDatos_Personales;
        public CN_DATOS_PERSONALES(IConfiguration configuration)
        {
            _CDDatos_Personales = new CD_DATOS_PERSONALES(configuration);
        }

        public void CN_Insertar_Datos_Personales(CE_DATOS_PERSONALES obj_Datos_Personales)
        {
            _CDDatos_Personales.InsertarDatosPersonales(obj_Datos_Personales);

        }
        public void CN_Actualizar_Datos_Personales(CE_DATOS_PERSONALES obj_Datos_Personales, out int resultado, out string mensaje)
        {
            _CDDatos_Personales.ActualizarDatosPersonales(obj_Datos_Personales, out resultado, out mensaje);

        }
        public List<CE_DATOS_PERSONALES> CN_Filtrar_Datos_Personales(CE_DATOS_PERSONALES obj_Datos_Personales)
        {
            return _CDDatos_Personales.FiltrarDatosPersonales(obj_Datos_Personales);
        }
        public List<CE_DATOS_PERSONALES> CN_Listar_Datos_Personales()
        {
            return _CDDatos_Personales.ListarDatosPersonales();
        }
        public List<CE_DATOS_PERSONALES> CN_Filtrar_Datos_PersonalesID(CE_DATOS_PERSONALES obj_Datos_Personales)
        {
            return _CDDatos_Personales.FiltrarDatosPersonalesID(obj_Datos_Personales);
        }
    }
}



