using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JWT.CORE.Repository
{
   public interface IGenericRepository<T> where T:class
    {
        Task<T> GetIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        void Remove(T entity);
        T Update(T entity);



    }
}
