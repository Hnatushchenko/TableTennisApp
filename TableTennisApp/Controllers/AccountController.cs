using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using TableTennisApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using TableTennisApp.Models;
using Microsoft.AspNetCore.Authentication;
using System.Net;
using TableTennisApp.Exceptions;

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
        public IActionResult Registration()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            ControllerContext.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromForm] string login, [FromForm] string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                return Unauthorized();
            }

            Player? player = _playerService.GetByLogin(login);
            if (player is null || player.Password != password)
            {
                return Unauthorized();
            }

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, login) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);
            await ControllerContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return Redirect("/");
        }

        
        [HttpPost]
        public async Task<IActionResult> Registration([FromForm] string name, [FromForm] string login, [FromForm] string password)
        {
            try
            {
                await _playerService.AddAsync(name, login, password);
            }
            catch (PlayerAlreadyExistsException)
            {
                // TODO: Handle the exception
                throw;
            }
            
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, login) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);
            await ControllerContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return Redirect("/");
        }
    }
}
