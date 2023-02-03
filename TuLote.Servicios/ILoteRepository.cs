using System.Linq.Expressions;

namespace TuLote.Abstracciones
{
    public interface ILoteRepository<T> where T : class
    {
        T GetFirst(
             Expression<Func<T, bool>> filtro = null,
             string incluirPropiedades = null,
             bool isTracking = true
             );
    }
}
