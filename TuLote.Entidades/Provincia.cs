using System.ComponentModel.DataAnnotations;

namespace TuLote.Entidades;
public class Provincia
{
    public int Id { get; set; }
    [Required(ErrorMessage = "La provincia es solicitada")]
    [Display(Name = "Provincia")]

    public string Nombre { get; set; }
    public List<Provincia> Provincias { get; set; }

    public ICollection<Municipio> Municipios { get; set; }
}

