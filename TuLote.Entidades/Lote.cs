using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuLote.Entidades
{

    public class Lote
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El número del lote es requerido")]
        [Display(Name = "Número de lote")]
        public string Numero { get; set; }
        [Required(ErrorMessage = "Los metros cuadrados son requeridos")]
        [Display(Name = "Metros (m2)")]
        public string Metros { get; set; }

        [EnumDataType(typeof(Tipo))]
        [Required(ErrorMessage = "La ubicación es requerida")]
        [Display(Name = "Ubicación")]
        public Tipo Tipo { get; set; }

        [EnumDataType(typeof(Orientaciones))]
        [Required(ErrorMessage = "La orientación es requerida")]
        public Orientaciones Orientacion { get; set; }

        public bool Disponible { get; set; }
        [Required(ErrorMessage = "Por favor cargue el monto")]
        [Display(Name = "Precio")]
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

    public enum Orientaciones
    {
        [Display(Name = "NORTE")] N = 1,
        [Display(Name = "SUR")] S = 2,
        [Display(Name = "NOROESTE")] NO = 3,
        [Display(Name = "NORESTE")] NE = 4,
        [Display(Name = "SUDESTE")] SE = 5,
        [Display(Name = "SUDOESTE")] SO = 6,
    }
    public enum Tipo
    {
        [Display(Name = "Interno")] Interno = 1,
        [Display(Name = "Perimetral")] Perimetral = 2,
        [Display(Name = "Laguna")] Laguna = 3,
        [Display(Name = "Rio")] Rio = 4,
        [Display(Name = "Bosque")] Bosque = 5,
        [Display(Name = "Golf")] Golf = 6
    }
}
