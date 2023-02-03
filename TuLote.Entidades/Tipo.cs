using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuLote.Entidades
{
    public class Tipo
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Tipo de orientación")]
        public string Nombre { get; set; }
        [ForeignKey("Lote")]
        public int Lote_Id { get; set; }
        public Lote Lote { get; set; }


    }
}
