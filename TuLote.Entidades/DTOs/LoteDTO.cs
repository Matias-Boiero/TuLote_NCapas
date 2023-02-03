using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuLote.Entidades.DTOs
{
    public class LoteDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El número del lote es requerido")]
        [Display(Name = "Número de lote")]
        public string Numero { get; set; }
        [Required(ErrorMessage = "Los metros cuadrados son requeridos")]
        [Display(Name = "Metros cuadrados")]
        public string Metros { get; set; }
        [Required(ErrorMessage = "La etapa es requerida")]
        [Display(Name = "Etapa/Área")]
        public string Etapa { get; set; }
        [EnumDataType(typeof(Orientacion))]
        public Orientacion Orientacion { get; set; }
        public bool Disponible { get; set; }
        [Required(ErrorMessage = "Por favor cargue el monto")]
        [Display(Name = "Precio en dolares")]
        public int Precio { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Creación del lote")]
        [Required(ErrorMessage = "Especifique una fecha")]
        public DateTime FechaCreacion { get; set; }
        [ForeignKey("Barrio")]
        public int Barrio_Id { get; set; }
        public Barrio Barrio { get; set; }
        [ForeignKey("Usuario")]
        public string Usuario_Id { get; set; }
        public Usuario Usuario { get; set; }

    }
}
