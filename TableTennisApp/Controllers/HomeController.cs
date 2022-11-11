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
        private readonly IPlayersService _playersService;
        public HomeController(IPlayersService playersService)
        {
            _playersService = playersService;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            if (User.Identity is not null && User.Identity.IsAuthenticated)
            {
                string? login = User.FindFirst(ClaimTypes.Name)?.Value;
                if (login is not null)
                {
                    Player? player = _playersService.GetByLogin(login);
                    return View(player);
                }
            }
            return Redirect("/Account/Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}