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
using System.Numerics;
using System.Runtime.InteropServices;

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
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.Email);
            if (user == null)
            {
                TempData["Error"] = "Не вдалося знайти ваш обліковий запис.";
                return View(loginVM);
            }

            var passwordIsValid = await _userManager.CheckPasswordAsync(user, loginVM.Password);
            if (passwordIsValid == false)
            {
                TempData["Error"] = "Неправильний пароль.";
                return View(loginVM);
            }

            var signingInResult = await _signInManager.PasswordSignInAsync(user, loginVM.Password, true, false);
            if (signingInResult.Succeeded)
            {
                return Redirect("/");
            }

            TempData["Error"] = "Щось пішло не так. Повторіть спробу.";
            return View(loginVM);
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
