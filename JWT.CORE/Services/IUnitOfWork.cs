using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JWT.CORE.Services
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        void Commit();
    }
}
