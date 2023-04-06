using Core.Entities;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        Task<T> GetByIDAsycn(int id);
        public Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string addProperties = null);
        Task<T> GetFirst(Expression<Func<T, bool>> filter = null, string addProperties = null);
        //Task<IReadOnlyList<T>> ListAllAsync();

       
    }
}
