using TableTennisApp.Models;

namespace TableTennisApp.Services
{
    public interface IQueueManager
    {
        void AddPlayer(Player player);
        void PutToEnd(Player player);
        void RemoveFirst();
        void RemovePlayer(Player player);
        void RemovePlayerByLogin(string login);
        void AddPlayerByLogin(string login);
        IEnumerable<Player> GetAllPlayers();
    }
}