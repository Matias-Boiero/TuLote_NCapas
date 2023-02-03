using System.ComponentModel.DataAnnotations;

namespace TuLote.Models.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress(ErrorMessage = "El mail debe ser válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "el campo {0} es requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Recuérdame")]
        public bool Recuerdame { get; set; }
    }
}
