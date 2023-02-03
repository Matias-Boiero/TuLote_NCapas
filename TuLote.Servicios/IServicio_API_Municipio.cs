
using TuLote.Entidades;


namespace TuLote.Servicios
{
    public interface IServicio_API_Municipio
    {
        Task<List<Municipio>> Lista();
    }
}
