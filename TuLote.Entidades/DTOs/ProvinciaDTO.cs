using System.ComponentModel.DataAnnotations;

namespace TuLote.Entidades.DTOs
{
    public class ProvinciaDTO
    {

        public int Id { get; set; }
        [Required]
        [Display(Name = "Provincia")]
        public string Nombre { get; set; }
        public List<Provincia> Provincias { get; set; }

        public ICollection<Municipio> Municipios { get; set; }
    }
}
