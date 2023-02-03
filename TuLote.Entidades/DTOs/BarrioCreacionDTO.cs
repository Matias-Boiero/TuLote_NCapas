using System.ComponentModel.DataAnnotations;

namespace TuLote.Entidades.DTOs
{
    public class BarrioCreacionDTO
    {
        public int Id { get; set; }
        [Display(Name = "Nombre del barrio")]
        public string Nombre { get; set; }
        public long Localidad_Id { get; set; }
    }
}
