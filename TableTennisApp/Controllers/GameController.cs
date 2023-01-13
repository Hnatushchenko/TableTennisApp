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

        [Authorize]
        [Route("Game/Index/{userId:guid?}")]
        [HttpGet]
        public async Task<IActionResult> Index(Guid userId)
        {
            IEnumerable<Game> games;
            if (User.IsInRole(UserRoles.Admin))
            {
                if (userId == Guid.Empty)
                {
                    games = await _gameService.GetAllGamesAsync();
                }
                else
                {
                    games = await _gameService.GetAllGamesByUserIdAsync(userId);
                }
                return View(games);
            }
            
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            if (user.Id != userId) return Forbid();

            games = await _gameService.GetAllGamesByUserIdAsync(userId);
            return View(games);
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
            return RedirectToAction(nameof(Add));
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Guid id)
        {
            await _gameService.DeleteAsync(id);
            return Ok();
        }
    }
}
