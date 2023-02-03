using System.ComponentModel.DataAnnotations;

namespace TuLote.Entidades.DTOs
{
    public class LocalidadCreacionDTO
    {
        public long Id { get; set; }
        [Display(Name = "Nombre de la localidad")]
        public string Nombre { get; set; }
        public int Municipio_Id { get; set; }

    }
}
