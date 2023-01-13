using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using TableTennisApp.Models;
using TableTennisApp.Services;

namespace TableTennisApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        public HomeController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            var model = await _userManager.GetUserDetailsByIdAsync(user.Id);
            return View("../Users/Details", model);
        }
    }
}