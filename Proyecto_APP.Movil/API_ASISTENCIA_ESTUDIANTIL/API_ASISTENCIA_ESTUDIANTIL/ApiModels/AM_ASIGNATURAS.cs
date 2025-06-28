namespace API_ASISTENCIA_ESTUDIANTIL.ApiModels
{
    public class AM_ASIGNATURAS
    {
        public int? Id_Asignatura { get; set; }
        public string? Nombre_Asignatura { get; set; }
        public string? Fecha_Creacion { get; set; }
        public string? Fecha_Modificacion { get; set; }
        public int? Id_Creador { get; set; }
        public int? Id_Modificador { get; set; }
        public int? Id_Estado { get; set; }
    }
}
