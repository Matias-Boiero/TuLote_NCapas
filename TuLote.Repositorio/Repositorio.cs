using System.Linq.Expressions;
using TuLote.Abstracciones;

namespace TuLote.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly IDbContext<T> _dbContext;

        public Repositorio(IDbContext<T> dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<T> Add(T entity)
        {
            return await _dbContext.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbContext.AddRange(entities);
        }

        public async Task<IEnumerable<T>> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate, string include)
        {
            return await _dbContext.Find(predicate, include);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.GetAll();
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeExpressions)
        {
            return _dbContext.GetAll(includeExpressions);
        }


        public async Task<T> GetById(int id)
        {
            return await _dbContext.GetById(id);
        }



        public void Remove(T entity)
        {
            _dbContext.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbContext.RemoveRange(entities);
        }

        public void Updtae(T entity)
        {
            _dbContext.Updtae(entity);
        }

        public async Task<IQueryable<T>> FindById(Expression<Func<T, bool>> expression)
        {
            return await _dbContext.FindById(expression);
        }
    }
}
