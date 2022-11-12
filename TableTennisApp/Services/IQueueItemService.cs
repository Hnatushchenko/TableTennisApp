using TableTennisApp.Models;

namespace TableTennisApp.Services
{
    public interface IQueueItemService
    {
        Task AddAsync(QueueItem queueItem);
        IEnumerable<QueueItem> GetQueueItems();
        IEnumerable<Player> GetPlayersFromQueue();
        Task RemoveByIdAsync(Guid id);
    }
}