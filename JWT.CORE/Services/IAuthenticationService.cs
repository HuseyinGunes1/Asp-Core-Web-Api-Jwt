using JWT.CORE.Dtos;
using JWT.SHARED.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JWT.CORE.Services
{
   public interface IAuthenticationService
    {
        Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto);
        Task<Response<TokenDto>> CreateTokenRefreshAsync(string rrefreshToken);
        Task<Response<NoDataDto>> RevokeAsync(string rrefreshToken);
        Response<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto);

    }
}
