using TableTennisApp.Models;

namespace TableTennisApp.Services
{
    public interface IPlayersService
    {
        IEnumerable<Player> GetAllPlayers();
        Task AddAsync(string name, string login, string password);
        Player? GetByLogin(string login);
    }
}