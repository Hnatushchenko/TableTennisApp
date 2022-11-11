using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TableTennisApp.Services;

namespace TableTennisApp.Controllers
{
    public class QueueController : Controller
    {
        private readonly IPlayersService _playersService;
        private readonly IQueueManager _queueManager;

        public QueueController(IPlayersService playersService, IQueueManager queueManager)
        {
            _playersService = playersService;
            _queueManager = queueManager;
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult Index()
        {
            var players = _queueManager.GetAllPlayers();
            return View(players);
        }

        
        [HttpGet]
        public IActionResult Enter()
        {
            if (User.Identity is not null && User.Identity.IsAuthenticated)
            {
                Claim? login = User.FindFirst(ClaimTypes.Name);
                if (login == null)
                {
                    throw new ArgumentException("Claim cannot be null");
                }
                _queueManager.RemovePlayerByLogin(login.Value);
                _queueManager.AddPlayerByLogin(login.Value);
                return Redirect("/Queue/Index"); // TODO: Add default path for Queue controller
            }
            else
            {
                return Redirect("/Account/Login");
            }
        }

        [HttpGet]
        public IActionResult Leave()
        {
            if (User.Identity is not null && User.Identity.IsAuthenticated)
            {
                Claim? login = User.FindFirst(ClaimTypes.Name);
                if (login == null)
                {
                    throw new ArgumentException("Claim cannot be null");
                }
                _queueManager.RemovePlayerByLogin(login.Value);
                return Redirect("/Queue/Index"); // TODO: Add default path for Queue controller
            }
            else
            {
                return Redirect("/Account/Login");
            }
        }
    }
}
