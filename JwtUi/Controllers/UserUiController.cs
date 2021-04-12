using JwtUi.Models;
using JwtUi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtUi.Controllers
{
    public class UserUiController : Controller
    {
        public readonly UiUserService _uiUserService;
        public UserUiController(UiUserService uiUserService)
        {
            _uiUserService = uiUserService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(UiUserDto uiUserService)
        {
            await _uiUserService.KullaniciEkle(uiUserService);
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Listele()
        {
            await _uiUserService.Listele();
            return View();
        }
    }
}
