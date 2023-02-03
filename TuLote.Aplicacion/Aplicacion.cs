using System.Linq.Expressions;

using TuLote.Repositorio;

namespace TuLote.Aplicacion
{
    public class Aplicacion<T> : IAplicacion<T> where T : class
    {
        private readonly IRepositorio<T> _repositorio;



        public Aplicacion(IRepositorio<T> repositorio)
        {
            _repositorio = repositorio;
        }

        //public void Add(T entity)
        //{
        //    _repositorio.Add(entity);
        //}


        public async Task<T> Add(T entity)
        {
            return await _repositorio.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _repositorio.AddRange(entities);
        }

        public async Task<IEnumerable<T>> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate, string include)
        {
            return await _repositorio.Find(predicate, include);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _repositorio.GetAll();
        }

        public async Task<T> GetById(int id)
        {
            return await _repositorio.GetById(id);
        }

        public void Remove(T entity)
        {
            _repositorio.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _repositorio.RemoveRange(entities);
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeExpressions)
        {
            return _repositorio.GetAll(includeExpressions);
        }

        public void Updtae(T entity)
        {
            _repositorio.Updtae(entity);
        }

        public async Task<IQueryable<T>> FindById(Expression<Func<T, bool>> expression)
        {
            return await _repositorio.FindById(expression);
        }

    }
}
