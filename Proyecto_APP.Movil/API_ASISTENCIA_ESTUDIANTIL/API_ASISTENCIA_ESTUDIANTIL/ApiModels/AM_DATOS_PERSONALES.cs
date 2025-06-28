namespace API_ASISTENCIA_ESTUDIANTIL.ApiModels
{
    public class AM_DATOS_PERSONALES
    {
        public int? Id_Persona { get; set; }
        public string? Primer_Nombre { get; set; }
        public string? Segundo_Nombre { get; set; }
        public string? Primer_Apellido { get; set; }
        public string? Segundo_Apellido { get; set; }
        public string? Edad { get; set; }
        public int? Tipo_Cargo { get; set; }
        public int? Tipo_DNI { get; set; }
        public string? DNI { get; set; }
        public int? Genero { get; set; }
        public int? Nacionalidad { get; set; }
        public int? Departamento { get; set; }
        public int? Estado_Civil { get; set; }
        public string? Fecha_Creacion { get; set; }
        public string? Fecha_Modificacion { get; set; }
        public int? Id_Creador { get; set; }
        public int? Id_Modificador { get; set; }
        public int? Id_Estado { get; set; }
    }
}
