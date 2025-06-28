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
    public class CN_TIPO_CATALOGOS
    {
        private readonly CD_TIPO_CATALOGOS _CD_Tipo_Catalogos;
        public CN_TIPO_CATALOGOS(IConfiguration configuration)
        {
            _CD_Tipo_Catalogos = new CD_TIPO_CATALOGOS(configuration);
        }

        public void CN_Insertar_Tipo_Catalogos(CE_TIPO_CATALOGOS obj)
        {
            _CD_Tipo_Catalogos.InsertarTipoCatalogo(obj);
        }

        public void CN_Actualizar_Tipo_Catalogos(CE_TIPO_CATALOGOS obj, out int resultado, out string mensaje)
        {
            _CD_Tipo_Catalogos.ActualizarTipoCatalogo(obj, out resultado, out mensaje);
        }

        public List<CE_TIPO_CATALOGOS> CN_Listar_Tipo_Catalogos()
        {
          return _CD_Tipo_Catalogos.ListarTipoCatalogos();
        }

        public List<CE_TIPO_CATALOGOS> CN_Filtrar_Tipo_Catalogos(CE_TIPO_CATALOGOS obj)
        {
            return _CD_Tipo_Catalogos.FiltrarTipoCatalogos(obj);
        }

        public List<CE_TIPO_CATALOGOS> CN_Filtrar_Tipo_CatalogosID(CE_TIPO_CATALOGOS obj)
        {
            return _CD_Tipo_Catalogos.FiltrarTipoCatalogosID(obj);
        }
    }
}
