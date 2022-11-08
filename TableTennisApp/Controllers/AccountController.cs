using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using TableTennisApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace TableTennisApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IPlayersService _playerService;

        public AccountController(IPlayersService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login([FromForm] string login, [FromForm] string password)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, login) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);
            SignIn(principal, CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration([FromForm] string name, [FromForm] string login, [FromForm] string password)
        {
            await _playerService.AddAsync(name, login, password);
            return View();
        }
    }
}
