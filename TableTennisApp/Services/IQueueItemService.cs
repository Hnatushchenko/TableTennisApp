using TableTennisApp.Models;

namespace TableTennisApp.Services
{
    public interface IQueueItemService
    {
        Task ClearAsync();
        Task AddAsync(QueueItem queueItem);
        IEnumerable<QueueItem> GetQueueItems();
        IEnumerable<ApplicationUser> GetPlayersFromQueue();
        Task RemoveByIdAsync(Guid id);
        Task<int> GetMaxOrdinalNumberAsync();
        Task RemoveByUserIdAsync(Guid id);
    }
}