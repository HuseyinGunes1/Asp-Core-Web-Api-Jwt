using JWT.CORE.Dtos;
using JWT.SHARED.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JWT.CORE.Services
{
   public interface IUserService
    {
        Task<Response<KullaniciDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<Response<KullaniciDto>>GetUserByNameAsync(string userName);
    }
}
