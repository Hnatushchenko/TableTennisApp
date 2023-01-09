using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TableTennisApp.Data.Constants;
using TableTennisApp.Data.ViewModels;
using TableTennisApp.Enums;
using TableTennisApp.Models;
using TableTennisApp.Services;

namespace TableTennisApp.Controllers
{
    public class GameController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly IGameService _gameService;

        public GameController(IGameService gameService, ApplicationUserManager userManager)
        {
            _gameService = gameService;
            _userManager = userManager;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var games = await _gameService.GetAllGamesAsync();
            return Json(games);
        }

        [Authorize(Roles = UserRoles.Referee)]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var players = await _userManager.Users.ToListAsync();
            var model = new GameVM
            {
                Players = players
            };
            return View(model);
        }

        [Authorize(Roles = UserRoles.Referee)]
        [HttpPost]
        public async Task<IActionResult> Add(GameVM gameVM)
        {
            if (!ModelState.IsValid)
            {
                gameVM.Players = await _userManager.Users.ToListAsync();
                return View(gameVM);
            }

            await _gameService.AddAsync(gameVM);
            return Redirect("/");
        }
    }
}
