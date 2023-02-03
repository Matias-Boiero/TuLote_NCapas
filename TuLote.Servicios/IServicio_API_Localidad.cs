using TuLote.Entidades;

namespace TuLote.Servicios
{
    public interface IServicio_API_Localidad
    {
        Task<List<Localidad>> Lista();
    }
}
