namespace API_ASISTENCIA_ESTUDIANTIL.ApiModels
{
    public class AM_USUARIOS
    {
        public int? Id_Usuario { get; set; }
        public int? Id_Persona { get; set; }
        public string? Usuario { get; set; }
        public string? Contrasena { get; set; }
        public string? Ultima_Sesion { get; set; }
        public string? Ultima_Cambio_Credenciales { get; set; }
        public int? Intentos_Sesion { get; set; }
        public string? Fecha_Creacion { get; set; }
        public string? Fecha_Modificacion { get; set; }
        public int? Id_Creador { get; set; }
        public int? Id_Modificador { get; set; }
        public int? Id_Estado { get; set; }
    }
}
