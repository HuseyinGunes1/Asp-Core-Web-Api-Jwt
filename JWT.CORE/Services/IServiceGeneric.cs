using JWT.SHARED.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JWT.CORE.Services
{
   public interface IServiceGeneric<T, TDto> where T : class where TDto : class
    {
        Task<Response<TDto>> GetIdAsync(int id);
        Task<Response<IEnumerable<TDto>>> GetAllAsync();
        Task<Response<IEnumerable<TDto>>> Where(Expression<Func<T, bool>> predicate);
        Task<Response<TDto>> AddAsync(TDto entity);
        Task<Response<NoDataDto>> Remove(int id);
        Task<Response<NoDataDto>> Update(TDto entity,int id);
    }
}
