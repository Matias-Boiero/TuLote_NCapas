using System.ComponentModel.DataAnnotations;

namespace TuLote.Enums
{
    public enum Orientaciones
    {
        [Display(Name = "NORTE")] N = 1,
        [Display(Name = "SUR")] S = 2,
        [Display(Name = "NOROESTE")] NO = 3,
        [Display(Name = "NORESTE")] NE = 4,
        [Display(Name = "SUDESTE")] SE = 5,
        [Display(Name = "SUDOESTE")] SO = 6,
    }
}
