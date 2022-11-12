using TableTennisApp.Models;

namespace TableTennisApp.Services
{
    public interface IQueueManager
    {
        IEnumerable<Player> GetAllPlayers();
        Task LeaveByLoginAsync(string login);
        Task EnterByLoginAsync(string login);
    }
}