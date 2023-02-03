using TuLote.Abstracciones;

namespace TuLote.Aplicacion
{
    public interface IAplicacion<T> : ICrud<T> where T : class
    {

    }
}
