using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TuLote.Abstracciones;
using TuLote.AccesoDatos;
using TuLote.Entidades;

namespace TuLote.Aplicacion
{
    public class LoteRepository : ILoteRepository<Lote>
    {
        private readonly ApplicationDbContext _context;
        DbSet<Lote> _dbcontext;
        public LoteRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbcontext = context.Set<Lote>();
        }

        public Lote GetFirst(Expression<Func<Lote, bool>> filtro = null, string incluirPropiedades = null, bool isTracking = true)
        {

            IQueryable<Lote> query = _dbcontext;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp);
                }
            }

            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            return query.FirstOrDefault();
        }
    }
}
