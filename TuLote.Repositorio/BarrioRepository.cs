using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TuLote.AccesoDatos;
using TuLote.Entidades;
using TuLote.Servicios;

namespace TuLote.Repositorio
{
    public class BarrioRepository : IBarrioRepositorio<Barrio>
    {
        private readonly ApplicationDbContext _context;
        DbSet<Barrio> _dbcontext;
        public BarrioRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbcontext = context.Set<Barrio>();
        }

        public Barrio GetFirst(Expression<Func<Barrio, bool>> filtro = null, string incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<Barrio> query = _dbcontext;
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
