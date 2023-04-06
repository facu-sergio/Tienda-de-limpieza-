using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly TiendaContext _context;
        private readonly DbSet<T> _dbset;

        public GenericRepository(TiendaContext context)
        {
            _context = context;
            _dbset = context.Set<T>();  
        }

        public async Task<T> GetByIDAsycn(int id)
        {
            return  await _context.Set<T>().FindAsync(id);
          
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string addProperties = null)
        {
            IQueryable<T> query = _dbset;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (addProperties != null)
            {
                //Busco separar las propiedades por la coma y elimina del areglo que devulve los elementos vacios
                foreach (var includePorperties in addProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includePorperties);

                }
                return query;
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetFirst(Expression<Func<T, bool>> filter = null, string addProperties = null)
        {
            IQueryable<T> query = _dbset;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (addProperties != null)
            {
                //Busco separar las propiedades por la coma y elimina del areglo que devulve los elementos vacios
                foreach (var includePorperties in addProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includePorperties);

                }
                return await query.FirstOrDefaultAsync();
            }

            return await query.FirstOrDefaultAsync();

        }

       
    }


}
