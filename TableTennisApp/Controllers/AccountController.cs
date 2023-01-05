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
using TableTennisApp.Data.ViewModels;
using TableTennisApp.Data.Constants;

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
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.Email);
            if (user != null)
            {
                TempData["Errors"] = new List<string> { "Ця електронна адреса вже використовується" };
                return View(registerVM);
            }

            var newUser = new ApplicationUser
            {
                UserName = registerVM.UserName,
                Email = registerVM.Email,
                Rating = 1200
            };

            IdentityResult result = await _userManager.CreateAsync(newUser, registerVM.Password);
            if (result.Succeeded == false)
            {
                TempData["Errors"] = result.Errors.Select(error => error.Description);
                return View(registerVM);
            }

            await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            return Redirect("/");
        }
    }
}
