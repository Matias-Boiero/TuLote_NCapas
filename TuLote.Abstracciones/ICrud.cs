using System.Linq.Expressions;

namespace TuLote.Abstracciones
{
    public interface ICrud<T> where T : class
    {

        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate, string include);
        Task<T> Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        IQueryable<T> GetAll(
        params Expression<Func<T, object>>[] includeExpressions);
        void Updtae(T entity);
        Task<IQueryable<T>> FindById(Expression<Func<T, bool>> expression);

    }
}
