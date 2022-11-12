using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TableTennisApp.Services;

namespace TableTennisApp.Controllers
{
    public class QueueController : Controller
    {
        private readonly IQueueManager _queueManager;

        public QueueController(IQueueManager queueManager)
        {
            _queueManager = queueManager;
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult Index()
        {
            var players = _queueManager.GetAllPlayers().ToList();
            return View(players);
        }
        
        [HttpGet]
        public async Task<IActionResult> Enter()
        {
            if (User.Identity is not null && User.Identity.IsAuthenticated)
            {
                Claim? login = User.FindFirst(ClaimTypes.Name);
                if (login == null)
                {
                    throw new ArgumentException("Claim cannot be null");
                }
                await _queueManager.EnterByLoginAsync(login.Value);
                return Redirect("/Queue/Index"); // TODO: Add default path for Queue controller
            }
            else
            {
                return Redirect("/Account/Login");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Leave()
        {
            if (User.Identity is not null && User.Identity.IsAuthenticated)
            {
                Claim? login = User.FindFirst(ClaimTypes.Name);
                if (login == null)
                {
                    throw new ArgumentException("Claim cannot be null");
                }
                await _queueManager.LeaveByLoginAsync(login.Value);
                return Redirect("/Queue/Index"); // TODO: Add default path for Queue controller
            }
            else
            {
                return Redirect("/Account/Login");
            }
        }
    }
}
