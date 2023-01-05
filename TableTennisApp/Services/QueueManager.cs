using Microsoft.AspNetCore.Identity;
using TableTennisApp.Models;

namespace TableTennisApp.Services
{
    public class QueueManager : IQueueManager
    {
        private readonly IQueueItemService _queueItemService;
        private readonly UserManager<ApplicationUser> _userManager;

        public QueueManager(IQueueItemService queueItemService, UserManager<ApplicationUser> userManager)
        {
            _queueItemService = queueItemService;
            _userManager = userManager;
        }
        public Task ClearAsync()
        {
            return _queueItemService.ClearAsync();
        }
        public IEnumerable<ApplicationUser> GetAllPlayers()
        {
            return _queueItemService.GetPlayersFromQueue();
        }
        public async Task LeaveByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            await _queueItemService.RemoveByUserIdAsync(user.Id);
        }
        public async Task EnterByEmailAsync(string email)
        {
            ApplicationUser? playerToAdd = await _userManager.FindByEmailAsync(email);

            await _queueItemService.RemoveByUserIdAsync(playerToAdd.Id);
            int maxOrdinalNumber = await _queueItemService.GetMaxOrdinalNumberAsync();
            
            QueueItem queueItem = new QueueItem()
            {
                OrdinalNumber = maxOrdinalNumber + 1,
                PlayerId = playerToAdd.Id,
            };

            await _queueItemService.AddAsync(queueItem);
        }
    }
}
