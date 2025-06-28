using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Dato;
using Capa_Entidades;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Capa_Negocio
{
    public class CN_Autor
    {
        private readonly CD_Autor _CD_Autor;

        public CN_Autor(IConfiguration configuration)
        {
            _CD_Autor = new CD_Autor(configuration);
        }

        public void _INSERTAR_Autor(CE_Autor obj_Estado)
        {
            _CD_Autor.Insertar_Autor(obj_Estado);
        }

        public void _Actualizar_Autor(CE_Autor obj_Estado)
        {
            _CD_Autor.Actualizar_Autor(obj_Estado);
        }

        public List<CE_Autor> Listar_Autores()
        {
            return _CD_Autor.Listar_Autores();
        }

        public List<CE_Autor> Filtrar_Autor(CE_Autor obj_Estado)
        {
            return _CD_Autor.Filtrar_Nombre(obj_Estado);
        }

        public CE_Autor Filtrar_Autor_Por_ID(int idAutor)
        {
            return _CD_Autor.Obtener_Por_Id(idAutor);
        }
    }
}

