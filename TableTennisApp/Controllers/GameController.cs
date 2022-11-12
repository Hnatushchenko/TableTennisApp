using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using TableTennisApp.Enums;
using TableTennisApp.Models;
using TableTennisApp.Services;

namespace TableTennisApp.Controllers
{
    public class GameController : Controller
    {
        private readonly IPlayersService _playersService;
        private readonly IGameService _gameService;

        public GameController(IPlayersService playersService, IGameService gameService)
        {
            _playersService = playersService;
            _gameService = gameService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            if (User.Identity is not null && User.Identity.IsAuthenticated)
            {
                Claim? loginClaim = User.FindFirst(ClaimTypes.Name);
                if (loginClaim == null)
                {
                    throw new ArgumentException("Claim cannot be null");
                }

                string currentUserLogin = User.FindFirstValue(ClaimTypes.Name);
                var players = _playersService.GetAllPlayers()
                    .Where(p => p.Login != currentUserLogin)
                    .OrderBy(p => p.Name);

                return View(players);
            }
            else
            {
                return Redirect("/Account/Login");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] GameResult gameResult, [FromForm] string opponentLogin)
        {
            if (User.Identity is not null && User.Identity.IsAuthenticated)
            {
                Claim? loginClaim = User.FindFirst(ClaimTypes.Name);
                if (loginClaim == null)
                {
                    throw new ArgumentException("Claim cannot be null");
                }

                string login = loginClaim.Value;
                if (gameResult == GameResult.Victory)
                {
                    await _gameService.AddAsync(login, opponentLogin);
                }
                else
                {
                    await _gameService.AddAsync(opponentLogin, login);
                }
                
                return Redirect("/"); // TODO: appropriate page
            }
            else
            {
                return Redirect("/Account/Login");
            }
        }
    }
}
