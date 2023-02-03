using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuLote.Entidades
{
    public class Localidad
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Elija una localidad")]
        [Display(Name = "Nombre de la localidad")]

        public string Nombre { get; set; }
        [ForeignKey("Municipio")]
        public int Municipio_Id { get; set; }
        public Municipio Municipio { get; set; }

        public List<Localidad> localidades { get; set; }
    }
}
