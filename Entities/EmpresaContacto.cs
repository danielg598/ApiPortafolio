namespace ApiPortafolio.Entities
{
    public class EmpresaContacto
    {
        public int Id { get; set; }
        public string NombreEmpresa { get; set; }
        public string NombreContacto { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
