using JWT.CORE.Dtos;
using JWT.CORE.Models;
using JWT.CORE.Services;
using JWT.SHARED.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.SERVİCE.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<Kullanici> _kullanici;
     
        public UserService(UserManager<Kullanici> userManager, IUnitOfWork unitOfWork)
        {
            _kullanici = userManager;
            
        }
        public async Task<Response<KullaniciDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new Kullanici { Email = createUserDto.EMail, UserName = createUserDto.UserName };
            var result = await _kullanici.CreateAsync(user, createUserDto.Password);
            if (result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return Response<KullaniciDto>.Fail(new ErrorDto(errors, true), 400);

            }
           
            return Response<KullaniciDto>.Success(ObjectMapper.MapperDeneme.Map<KullaniciDto>(user),200);
            
        }

        public async Task<Response<KullaniciDto>> GetUserByNameAsync(string userName)
        {
          var user= await _kullanici.FindByNameAsync(userName);
            if (user == null)
            {
                return Response<KullaniciDto>.Fail("user name not found",404,true);

            }
            return Response<KullaniciDto>.Success(ObjectMapper.MapperDeneme.Map<KullaniciDto>(user), 200);
        }
    }
}
