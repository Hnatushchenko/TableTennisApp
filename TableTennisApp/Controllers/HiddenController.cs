using Microsoft.AspNetCore.Mvc;
using TableTennisApp.Services;

namespace TableTennisApp.Controllers
{
    public class HiddenController : Controller
    {
        private readonly IQueueItemService _queueItemService;
        private readonly IPlayersService _playersService;
        public HiddenController(IPlayersService playersService, IQueueItemService queueItemService)
        {
            _playersService = playersService;
            _queueItemService = queueItemService;
        }

        public IActionResult Players()
        {
            return Json(_playersService.GetAllPlayers());
        }

        public IActionResult QueueItems()
        {
            return Json(_queueItemService.GetQueueItems().Select(q => new
            {
                q.Id,
                q.OrdinalNumber,
                q.Player.Name,
                q.Player.Login
            }));
        }
    }
}
