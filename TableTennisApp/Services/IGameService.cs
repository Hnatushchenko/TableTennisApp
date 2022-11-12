using TableTennisApp.Models;

namespace TableTennisApp.Services
{
    public interface IGameService
    {
        Task AddAsync(string playerWhoWonLogin, string playerWhoLostLogin);
        IEnumerable<Game> GetAllGames();
    }
}