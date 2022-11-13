using TableTennisApp.Models;

namespace TableTennisApp.Services
{
    public interface IQueueManager
    {
        Task ClearAsync();
        IEnumerable<Player> GetAllPlayers();
        Task LeaveByLoginAsync(string login);
        Task EnterByLoginAsync(string login);
    }
}