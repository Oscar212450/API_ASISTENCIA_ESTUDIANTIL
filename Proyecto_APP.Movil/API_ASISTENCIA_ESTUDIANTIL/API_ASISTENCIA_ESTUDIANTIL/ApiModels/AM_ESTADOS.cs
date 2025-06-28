namespace API_ASISTENCIA_ESTUDIANTIL.ApiModels
{
    public class AM_ESTADOS
    {
        public int? Id_Estados { get; set; }
        public string? Estado { get; set; }
        public string? Fecha_Creacion { get; set; }
        public string? Fecha_Modificacion { get; set; }
        public int? Id_Creador { get; set; }
        public int? Id_Modificador { get; set; }
        public bool? Activo { get; set; }
        
    }
}
