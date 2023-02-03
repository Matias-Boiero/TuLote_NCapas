using System.ComponentModel.DataAnnotations;

namespace TuLote.Models.ViewModel
{
    public class RegistroVM
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Apellido { get; set; }
        [MaxLength(3)]
        public string Alias { get; set; }
        public string Telefono { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress(ErrorMessage = "El mail debe ser válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }
    }
}
