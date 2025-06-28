namespace API_ASISTENCIA_ESTUDIANTIL.ApiModels
{
    public class AM_HORARIOS
    {
        public int Id_Horario { get; set; }
        public int? Id_Turno { get; set; }
        public string? Horario { get; set; }
        public string? Hora_Inicio { get; set; }
        public string? Hora_Fin { get; set; }
        public string? Fecha_Creacion { get; set; }
        public string? Fecha_Modificacion { get; set; }
        public int? Id_Creador { get; set; }
        public int? Id_Modificador { get; set; }
        public int? Id_Estado { get; set; }
    }
}
