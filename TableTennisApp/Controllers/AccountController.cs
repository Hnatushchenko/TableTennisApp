using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using TableTennisApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using TableTennisApp.Models;
using Microsoft.AspNetCore.Authentication;
using System.Net;
using TableTennisApp.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace TableTennisApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IPlayersService _playerService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IPlayersService playerService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _playerService = playerService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
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
            //if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            //{
            //    return Redirect("/Account/Login"); // TODO: Send the message, wrong password
            //}

            //login = login.Trim();
            //password = password.Trim();
            //Player? player = _playerService.GetByLogin(login);
            //if (player is null || player.Password != password)
            //{
            //    return Redirect("/Account/Login"); // TODO: Send the message, wrong password
            //}

            //var claims = new List<Claim> { new Claim(ClaimTypes.Name, login) };
            //ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);
            //await ControllerContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return Redirect("/");
        }

        
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] string name, [FromForm] string email, [FromForm] string password)
        {

            try
            {
                await _playerService.AddAsync(name, login.Trim(), password.Trim());
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
