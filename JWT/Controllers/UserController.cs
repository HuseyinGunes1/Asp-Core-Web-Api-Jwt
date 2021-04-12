using JWT.CORE.Dtos;
using JWT.CORE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            return ActionInstance(await _userService.CreateUserAsync(createUserDto));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            return ActionInstance(await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name));
        }
    }
}
