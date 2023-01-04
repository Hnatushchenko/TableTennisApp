using TableTennisApp.Models;

namespace TableTennisApp.Services
{
    public class QueueManager : IQueueManager
    {
        private readonly IQueueItemService _queueItemService;
        private readonly IPlayersService _playersService;

        public QueueManager(IPlayersService playersService, IQueueItemService queueItemService)
        {
            _playersService = playersService;
            _queueItemService = queueItemService;
        }
        public async Task ClearAsync()
        {
            await _queueItemService.ClearAsync();
        }
        public IEnumerable<ApplicationUser> GetAllPlayers()
        {
            return _queueItemService.GetPlayersFromQueue();
        }
        public async Task LeaveByLoginAsync(string login)
        {
            //var queueItem = _queueItemService.GetQueueItems().
            //    SingleOrDefault(queueItem => queueItem.Player.Login == login);

            //if (queueItem is null)
            //{
            //    return;
            //}
            //await _queueItemService.RemoveByIdAsync(queueItem.Id);

        }
        public async Task EnterByLoginAsync(string login)
        {

            ApplicationUser? playerToAdd = _playersService.GetByLogin(login);
            if (playerToAdd is null)
            {
                throw new ArgumentException("Invalid login");
            }

            await LeaveByLoginAsync(login);
            int maxOrdinalNumber = 1;
            try
            {
                maxOrdinalNumber = _queueItemService.GetQueueItems().Max(queueItem => queueItem.OrdinalNumber);
            }
            catch (Exception)
            {
            }

            QueueItem queueItem = new QueueItem()
            {
                Id = Guid.NewGuid(),
                OrdinalNumber = maxOrdinalNumber + 1,
                PlayerId = playerToAdd.Id,
            };

            await _queueItemService.AddAsync(queueItem);
        }
    }
}
