using System.ComponentModel.DataAnnotations;

namespace TuLote.Entidades.DTOs
{
    public class ProvinciaCreacionDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "La provincia es necesaria")]
        public string Nombre { get; set; }
    }
}
