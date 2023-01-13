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

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<IActionResult> Clear()
        {
            await _queueManager.ClearAsync();
            return Redirect("/Queue/Index");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Enter()
        {
            Claim? email = User.FindFirst(ClaimTypes.Email);
            await _queueManager.EnterByEmailAsync(email?.Value);
            return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Leave()
        {
            Claim? login = User.FindFirst(ClaimTypes.Email);
            await _queueManager.LeaveByEmailAsync(login?.Value);
            return Ok();
        }
    }
}
