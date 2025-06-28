using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CE_HORARIOS
    {
        public int? Id_Horario { get; set; }
        public int? Id_Turno { get; set; }
        public string? Horario { get; set; }
        public string? Hora_Inicio { get; set; }
        public string? Hora_Fin {  get; set; }
        public string? Fecha_Creacion { get; set; }
        public string? Fecha_Modificacion { get; set; }
        public int? Id_Creador { get; set; }
        public int? Id_Modificador { get; set; }
        public int? Id_Estado { get; set; }
    }
}
