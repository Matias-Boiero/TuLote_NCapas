using System.ComponentModel.DataAnnotations;

namespace TuLote.Enums
{
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
