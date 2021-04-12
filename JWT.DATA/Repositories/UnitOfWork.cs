using JWT.CORE.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JWT.DATA.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext dbContext;
        
        public UnitOfWork(ProjeDbContext context)
        {
            dbContext = context;
        }
        public void Commit()
        {
            dbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
