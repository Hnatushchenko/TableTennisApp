using Microsoft.AspNetCore.Mvc;
using TableTennisApp.Services;

namespace TableTennisApp.Controllers
{
    public class HiddenController : Controller
    {
        private readonly IQueueItemService _queueItemService;
        private readonly IPlayersService _playersService;
        private readonly IGameService _gameService;
        public HiddenController(IPlayersService playersService, IQueueItemService queueItemService, IGameService gameService)
        {
            _playersService = playersService;
            _queueItemService = queueItemService;
            _gameService = gameService;
        }


        public IActionResult Games()
        {
            var games = _gameService.GetAllGames();
            return Json(games);
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
                q.Player.UserName,
                q.Player.Email
            }));
        }
    }
}
