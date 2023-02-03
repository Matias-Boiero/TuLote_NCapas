using System.ComponentModel.DataAnnotations;

namespace TuLote.Entidades.DTOs
{
    public class MunicipioCreacionDTO
    {
        public int Id { get; set; }


        [Display(Name = "Municipio")]
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int Provincia_Id { get; set; }




    }
}

