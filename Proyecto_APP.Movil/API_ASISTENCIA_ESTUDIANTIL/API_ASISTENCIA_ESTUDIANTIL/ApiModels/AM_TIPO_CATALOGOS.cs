namespace API_ASISTENCIA_ESTUDIANTIL.ApiModels
{
    public class AM_TIPO_CATALOGOS
    {
        public int? Id_Tipo_Catalogo { get; set; }
        public string? Tipo_Catalogo { get; set; }
        public string? Fecha_Creacion { get; set; }
        public string? Fecha_Modificacion { get; set; }
        public int? Id_Creador { get; set; }
        public int? Id_Modificador { get; set; }
        public int? Activo { get; set; }
    }
}
