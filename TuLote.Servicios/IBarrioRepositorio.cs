using System.Linq.Expressions;

namespace TuLote.Servicios
{
    public interface IBarrioRepositorio<T> where T : class
    {
        T GetFirst(
            Expression<Func<T, bool>> filtro = null,
            string incluirPropiedades = null,
            bool isTracking = true
            );
    }
}
