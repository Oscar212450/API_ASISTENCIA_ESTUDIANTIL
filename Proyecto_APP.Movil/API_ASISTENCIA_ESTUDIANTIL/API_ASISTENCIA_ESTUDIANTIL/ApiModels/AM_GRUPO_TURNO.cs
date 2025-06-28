namespace API_ASISTENCIA_ESTUDIANTIL.ApiModels
{
    public class AM_GRUPO_TURNO
    {
        public int? Id_Grupo_Turno { get; set; }
        public int? Id_Grupo { get; set; }
        public int? Id_Horario { get; set; }
        public string? Fecha_Creacion { get; set; }
        public string? Fecha_Modificacion { get; set; }
        public int? Id_Creador { get; set; }
        public int? Id_Modificador { get; set; }
        public int? Id_Estado { get; set; }
    }
}
