using JWT.CORE.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JWT.DATA.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext dbContext;
        private readonly DbSet<T> dbset;
        public GenericRepository(ProjeDbContext context)
        {
            dbContext = context;
            dbset = dbContext.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await dbset.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbset.ToListAsync();
        }

        public async Task<T> GetIdAsync(int id)
        {
            var entity = await dbset.FindAsync(id);
            if (entity != null)
            {
                dbContext.Entry(entity).State = EntityState.Detached;
            }
            return entity;
        }

        public void Remove(T entity)
        {
            dbset.Remove(entity);
        }

        public T Update(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return dbset.Where(predicate);
        }
    }
}
