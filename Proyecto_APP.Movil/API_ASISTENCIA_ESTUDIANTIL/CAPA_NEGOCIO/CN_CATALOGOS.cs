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
    public class CN_CATALOGOS
    {
        private readonly CD_CATALOGOS _CDCatalogos;
        public CN_CATALOGOS(IConfiguration configuration)
        {
            _CDCatalogos = new CD_CATALOGOS(configuration);
        }

        public void CN_Insertar_Catalogos(CE_CATALOGOS obj_Catalogos)
        {
            _CDCatalogos.InsertarCatalogo(obj_Catalogos);

        }
        public void CN_Actualizar_Catalogos(CE_CATALOGOS obj_Catalogos, out int resultado, out string mensaje)
        {
            _CDCatalogos.ActualizarCatalogo(obj_Catalogos, out resultado, out mensaje);

        }
        public List<CE_CATALOGOS> CN_Filtrar_Catalogos(CE_CATALOGOS obj_Catalogos)
        {
            return _CDCatalogos.FiltrarCatalogos(obj_Catalogos);
        }
        public List<CE_CATALOGOS> CN_Listar_Catalogos()
        {
            return _CDCatalogos.ListarCatalogos();
        }
        public List<CE_CATALOGOS> CN_Filtrar_CatalogosID(CE_CATALOGOS obj_Catalogos)
        {
            return _CDCatalogos.FiltrarCatalogosID(obj_Catalogos);
        }
    }
}
