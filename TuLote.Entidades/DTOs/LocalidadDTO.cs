using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuLote.Entidades.DTOs
{
    public class LocalidadDTO
    {
        public long Id { get; set; }
        [Display(Name = "Nombre de la localidad")]
        [Required(ErrorMessage = "Elija una localidad")]
        [Remote("IsAlreadyExist", "Localidades", HttpMethod = "post")]
        public string Nombre { get; set; }
        [ForeignKey("Municipio")]
        public int Municipio_Id { get; set; }
        public Municipio Municipio { get; set; }

        public List<Localidad> localidades { get; set; }
    }
}
