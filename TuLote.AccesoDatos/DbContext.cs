using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TuLote.Abstracciones;

namespace TuLote.AccesoDatos
{
    public class DbContext<T> : IDbContext<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        DbSet<T> _context;

        public DbContext(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _context = dbContext.Set<T>();
        }

        public async Task<T> Add(T entity)
        {
            await _context.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.AddRangeAsync(entities);
            _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate, string include)
        {
            return _context.Include(include).Where(predicate);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.ToListAsync();

        }

        public async Task<T> GetById(int id)
        {
            return await _context.FindAsync(id);

        }

        public void Remove(T entity)
        {
            _context.Remove(entity);
            _dbContext.SaveChanges();
        }

        public async void RemoveRange(IEnumerable<T> entities)
        {
            _context.RemoveRange(entities);
            _dbContext.SaveChanges();
        }



        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> set = _context;

            foreach (var includeExpression in includeExpressions)
            {
                set = set.Include(includeExpression);
            }
            return set;
        }

        public void Updtae(T entity)
        {
            _context.Update(entity);
            _dbContext.SaveChanges();
        }

        public async Task<IQueryable<T>> FindById(Expression<Func<T, bool>> expression)
        {
            return _context.Where(expression);
        }
    }
}
