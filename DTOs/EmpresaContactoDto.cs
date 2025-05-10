using System.ComponentModel.DataAnnotations;

namespace ApiPortafolio.DTOs
{
    public class EmpresaContactoDto
    {
        [Required]
        public string NombreEmpresa { get; set; }
        [Required]
        public string NombreContacto { get; set; }
        [Required]
        [EmailAddress]
        public string Correo { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public string Mensaje { get; set; }
    }
}
