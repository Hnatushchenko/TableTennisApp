using TableTennisApp.Models;

namespace TableTennisApp.Services
{
    public interface IQueueManager
    {
        Task ClearAsync();
        IEnumerable<ApplicationUser> GetAllPlayers();
        Task LeaveByLoginAsync(string login);
        Task EnterByLoginAsync(string login);
    }
}