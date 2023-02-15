using System.ComponentModel.DataAnnotations;

namespace TuLote.Enums
{
    public enum Tipo
    {
        Interno = 1,
        Perimetral = 2,
        Laguna = 3,
        Rio = 4,
        Bosque = 5,
        Golf = 6,
        [Display(Name = "Espejo de agua")] Espejo_de_Agua = 7,
        Abierto = 8,
        Semicerrado = 9
    }
}
