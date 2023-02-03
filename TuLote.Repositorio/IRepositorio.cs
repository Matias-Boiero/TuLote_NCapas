using TuLote.Abstracciones;

namespace TuLote.Repositorio
{
    public interface IRepositorio<T> : ICrud<T> where T : class
    {

    }
}
