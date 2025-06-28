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
    public class CN_USUARIOS
    {
        private readonly CD_USUARIOS _CDUsuarios;

        public CN_USUARIOS(IConfiguration configuration)
        {
            _CDUsuarios = new CD_USUARIOS(configuration);
        }

        public void CN_Insertar_Usuarios(CE_USUARIOS obj_Usuarios, out int resultado, out string mensaje)
        {
            _CDUsuarios.InsertarUsuarios(obj_Usuarios, out resultado, out mensaje);
        }

        public void CN_Actualizar_Usuarios(CE_USUARIOS obj_Usuarios)
        {
            _CDUsuarios.ActualizarUsuarios(obj_Usuarios);
        }

        public List<CE_USUARIOS> CN_Listar_Usuarios()
        {
            return _CDUsuarios.ListarUsuarios();
        }

        public List<CE_USUARIOS> CN_Filtrar_Usuarios(CE_USUARIOS obj_Usuarios)
        {
            return _CDUsuarios.FiltrarUsuarios(obj_Usuarios);
        }
        public List<CE_USUARIOS> CN_Filtrar_UsuariosID(CE_USUARIOS obj_Usuarios)
        {
            return _CDUsuarios.FiltrarUsuariosID(obj_Usuarios);
        }
    }

}
