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
    public class CN_ESTUDIANTES
    {
        private readonly CD_ESTUDIANTES _CD_Estudiantes;

        public CN_ESTUDIANTES(IConfiguration configuration)
        {
            _CD_Estudiantes = new CD_ESTUDIANTES(configuration);
        }

        public void CN_Insertar_Estudiante(CE_ESTUDIANTES obj_estudiantes)
        {
            _CD_Estudiantes.InsertarEstudiantes(obj_estudiantes);
        }

        public void CN_Actualizar_Estudiante(CE_ESTUDIANTES obj_estudiantes, out int resultado, out string mensaje)
        {
            _CD_Estudiantes.ActualizarEstudiantes(obj_estudiantes, out resultado, out mensaje);
        }

        public List<CE_ESTUDIANTES> CN_Listar_Estudiantes()
        {
            return _CD_Estudiantes.ListarEstudiantes();
        }
        public List<CE_ESTUDIANTES> CN_Filtrar_Estudiantes(CE_ESTUDIANTES obj_estudiantes)
        {
            return _CD_Estudiantes.FiltrarEstudiantes(obj_estudiantes);
        }
        public List<CE_ESTUDIANTES> CN_Filtrar_EstudiantesID(CE_ESTUDIANTES obj_estudiantes)
        {
            return _CD_Estudiantes.FiltrarEstudiantes(obj_estudiantes);
        }
    }
}
