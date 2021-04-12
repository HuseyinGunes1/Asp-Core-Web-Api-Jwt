using JWT.CORE.Dtos;
using JWT.CORE.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : CustomBaseController
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken(LoginDto loginDto)
        {
            var result = await _authenticationService.CreateTokenAsync(loginDto);
            return ActionInstance(result);
        }

        [HttpPost]
        public IActionResult CreateClientToke(ClientLoginDto clientloginDto)
        {
            var result =  _authenticationService.CreateTokenByClient(clientloginDto);
            return ActionInstance(result);
        }

        [HttpPost]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var result = await _authenticationService.RevokeAsync(refreshTokenDto.RefreshToken);
            return ActionInstance(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateByRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var result = await _authenticationService.CreateTokenRefreshAsync(refreshTokenDto.RefreshToken);
            return ActionInstance(result);
        }
    }
}
