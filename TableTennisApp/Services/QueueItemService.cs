using Microsoft.EntityFrameworkCore;
using TableTennisApp.Models;
using TableTennisApp.Repository;

namespace TableTennisApp.Services
{
    public class QueueItemService : IQueueItemService
    {
        private readonly IApplicationContext _dbContext;

        public QueueItemService(IApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<QueueItem> GetQueueItems()
        {
            return _dbContext.QueueItems.Include(i => i.Player);
        }

        public IEnumerable<Player> GetPlayersFromQueue()
        {
            return _dbContext.QueueItems
                .Include(i => i.Player)
                .OrderBy(x => x.OrdinalNumber)
                .Select(q => q.Player).AsEnumerable();
        }

        public async Task AddAsync(QueueItem queueItem)
        {
            _dbContext.QueueItems.Add(queueItem);
            await _dbContext.SaveChangesAsync();
            var items = _dbContext.QueueItems.ToList();
        }

        public async Task RemoveByIdAsync(Guid id)
        {
            var item = _dbContext.QueueItems.Single(q => q.Id == id);
            _dbContext.QueueItems.Remove(item);
            await _dbContext.SaveChangesAsync();
        }
    }
}
