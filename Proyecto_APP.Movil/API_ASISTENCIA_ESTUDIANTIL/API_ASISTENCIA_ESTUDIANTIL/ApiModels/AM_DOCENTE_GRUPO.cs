namespace API_ASISTENCIA_ESTUDIANTIL.ApiModels
{
    public class AM_DOCENTE_GRUPO
    {
        public int? Id_Docente_Grupo { get; set; }
        public int? Id_Docente { get; set; }
        public int? Id_Grupo { get; set; }
        public string? Fecha_Creacion { get; set; }
        public string? Fecha_Modificacion { get; set; }
        public int? Id_Creador { get; set; }
        public int? Id_Modificador { get; set; }
        public int? Id_Estado { get; set; }
    }
}
