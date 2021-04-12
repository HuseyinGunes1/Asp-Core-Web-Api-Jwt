using JWT.CORE.Configuration;
using JWT.CORE.Dtos;
using JWT.CORE.Models;
using JWT.CORE.Repository;
using JWT.CORE.Services;
using JWT.SHARED.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.SERVİCE.Services
{
    public class AuthenticationServices : IAuthenticationService
    {
        private readonly List<Client> clients;
        private readonly ITokenServices tokenService;
        private readonly UserManager<Kullanici> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IGenericRepository<RefreshToken> refreshToken;
       
        public AuthenticationServices(IOptions<List<Client>> _clients, ITokenServices _tokenService, UserManager<Kullanici> _userManager, IUnitOfWork _unitOfWork, IGenericRepository<RefreshToken> _RefreshToken)
        {
            clients = _clients.Value;
            tokenService = _tokenService;
            userManager = _userManager;
            unitOfWork = _unitOfWork;
            refreshToken = _RefreshToken;
        }


        public async Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Response<TokenDto>.Fail("Email veya parola yanlış", 404, true);

            if (!await userManager.CheckPasswordAsync(user, loginDto.PassWord))
            {
               return Response<TokenDto>.Fail("Email veya parola yanlış", 404, true);
            }

            var token = tokenService.CreateToken(user);
            var UserRefreshToken = await refreshToken.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

            if (UserRefreshToken == null)
            {
                await refreshToken.AddAsync(new RefreshToken { UserId = user.Id, Code = token.RefreshToken, OTraih = token.RefreshTokenExpiration });
            }
            else
            {
                UserRefreshToken.Code = token.RefreshToken;
                UserRefreshToken.OTraih = token.RefreshTokenExpiration;

            }
            await unitOfWork.CommitAsync();
            return Response<TokenDto>.Success(token,200);
        }

        public Response<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var _Client = clients.SingleOrDefault(x => x.Id == clientLoginDto.ClientId && x.Secret == clientLoginDto.ClientSecret);
            if (_Client == null)
            {
                return Response<ClientTokenDto>.Fail("client bulunamadı", 404, true);
            }
            var token = tokenService.CreateToken(_Client);
            return Response<ClientTokenDto>.Success(token, 200);

        }
        public async Task<Response<TokenDto>> CreateTokenRefreshAsync(string rrefreshToken)
        {
            var existRefreshToken = await refreshToken.Where(x => x.Code == rrefreshToken).SingleOrDefaultAsync();
            if (existRefreshToken == null)
            {
                return Response<TokenDto>.Fail("refresh token bulunamadı", 404, true);
            }
            var user = await userManager.FindByIdAsync(existRefreshToken.UserId);
            if (user == null)
            {
                return Response<TokenDto>.Fail("refresh token bulunamadı", 404, true);
            }

            var tokenDto = tokenService.CreateToken(user);
            existRefreshToken.Code = tokenDto.RefreshToken;
            existRefreshToken.OTraih = tokenDto.RefreshTokenExpiration;
            await unitOfWork.CommitAsync();
            return Response<TokenDto>.Success(tokenDto, 200);
        }

        public async Task<Response<NoDataDto>> RevokeAsync(string rrefreshToken)
        {
            var existRefreshToken = await refreshToken.Where(x => x.Code == rrefreshToken).SingleOrDefaultAsync();
            if (existRefreshToken == null)
            {
                return Response<NoDataDto>.Fail("refresh token bulunamadı", 404, true);
            }

            refreshToken.Remove(existRefreshToken);

            await unitOfWork.CommitAsync();
            return Response<NoDataDto>.Success(200);
        }
    }
}
