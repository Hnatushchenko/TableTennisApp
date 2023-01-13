using Microsoft.EntityFrameworkCore;
using TableTennisApp.Data.Repository;
using TableTennisApp.Models;

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

        public async Task<int> GetMaxOrdinalNumberAsync()
        {
            if (await _dbContext.QueueItems.AnyAsync())
            {
                int maxOrdinalNumber = await _dbContext.QueueItems.MaxAsync(item => item.OrdinalNumber);
                return maxOrdinalNumber;
            }
            return 0;
        }

        public async Task ClearAsync()
        {
            foreach (QueueItem queueItem in _dbContext.QueueItems)
            {
                _dbContext.QueueItems.Remove(queueItem);
            }
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<ApplicationUser> GetPlayersFromQueue()
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
        }

        public async Task RemoveByIdAsync(Guid id)
        {
            var item = _dbContext.QueueItems.Single(q => q.Id == id);
            _dbContext.QueueItems.Remove(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveByUserIdAsync(Guid id)
        {
            var item = await _dbContext.QueueItems.SingleOrDefaultAsync(q => q.PlayerId == id);
            if (item is not null)
            {
                _dbContext.QueueItems.Remove(item);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
