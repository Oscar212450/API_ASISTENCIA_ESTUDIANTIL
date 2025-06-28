namespace API_ASISTENCIA_ESTUDIANTIL.ApiModels
{
    public class AM_CONTACTOS
    {
        public int? Id_Contacto { get; set; }
        public int? Id_Persona { get; set; }
        public int? Tipo_Contacto { get; set; }
        public string? Contacto { get; set; }
        public string? Codigo_Postal { get; set; }
        public int? Pais { get; set; }
        public int? Id_Creador { get; set; }
        public int? Id_Modificador { get; set; }
        public string? Fecha_Creacion { get; set; }
        public string? Fecha_Modificacion { get; set; }
        public int? Id_Estado { get; set; }
    }
}
