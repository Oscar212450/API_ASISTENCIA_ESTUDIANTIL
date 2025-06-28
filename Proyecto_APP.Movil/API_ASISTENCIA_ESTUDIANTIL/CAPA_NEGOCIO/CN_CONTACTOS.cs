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
    public class CN_CONTACTOS
    {
        private readonly CD_CONTACTOS _Cd_Contactos;

        public CN_CONTACTOS(IConfiguration configuration)
        {
            _Cd_Contactos = new CD_CONTACTOS(configuration);
        }

        public void CN_Insertar_Contactos(CE_CONTACTOS obj_Contactos)
        {
            _Cd_Contactos.InsertarContacos(obj_Contactos);
        }
        public void CN_Actualizar_Contactos(CE_CONTACTOS obj_Contactos, out int resultado, out string mensaje)
        {
            _Cd_Contactos.ActualizarContactos(obj_Contactos, out resultado, out mensaje);
        }
        public List<CE_CONTACTOS> CN_Obtener_Contactos()
        {
            return _Cd_Contactos.ListarContactos();
        }
        public List<CE_CONTACTOS> CN_Filtrar_Contactos(CE_CONTACTOS obj_Contactos)
        {
            return _Cd_Contactos.FiltrarContactos(obj_Contactos);
        }
        public List<CE_CONTACTOS> CN_Filtrar_ContactosID(CE_CONTACTOS obj_Contactos)
        {
            return _Cd_Contactos.FiltrarContactosID(obj_Contactos);
        }
    }
}
