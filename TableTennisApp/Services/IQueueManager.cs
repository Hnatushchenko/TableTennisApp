using TableTennisApp.Models;

namespace TableTennisApp.Services
{
    public interface IQueueManager
    {
        Task ClearAsync();
        IEnumerable<ApplicationUser> GetAllPlayers();
        Task LeaveByEmailAsync(string login);
        Task EnterByEmailAsync(string login);
    }
}