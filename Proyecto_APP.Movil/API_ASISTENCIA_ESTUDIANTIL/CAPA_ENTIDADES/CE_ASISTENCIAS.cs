using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CE_ASISTENCIAS
    {
        public int? Id_Asistencia {  get; set; }
        public int? Id_Estudiante_Grupo { get; set; }
        public int? Id_Grupo_Asignatura { get; set; }
        public int? Id_Docente_Grupo { get; set; }
        public string? Fecha { get; set; }
        public string? Asistio { get; set; }
        public string? Observacion { get; set; }
        public string? Fecha_Creacion { get; set; }
        public string? Fecha_Modificacion { get; set; }
        public int? Id_Creador { get; set; }
        public int? Id_Modificador { get; set; }
        public int? Id_Estado { get; set; }
    }
}
