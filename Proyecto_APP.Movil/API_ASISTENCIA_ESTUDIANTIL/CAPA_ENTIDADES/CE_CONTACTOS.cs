using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CE_CONTACTOS
    {
       public int? Id_Contacto { get; set; }
       public int? Id_Persona { get; set; }
       public int? Tipo_Contacto { get; set; }
       public string Contacto { get; set; }
       public string Codigo_Postal { get; set; }
       public int? Pais { get; set; }
        public int? Id_Creador { get; set; }
        public int? Id_Modificador { get; set; }
        public string Fecha_Creacion { get; set; }
       public string Fecha_Modificacion { get; set; }
       public int? Id_Estado { get; set; }
    }
}
