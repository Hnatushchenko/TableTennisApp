using TableTennisApp.Models;

namespace TableTennisApp.Services
{
    public interface IPlayersService
    {
        IEnumerable<ApplicationUser> GetAllPlayers();
        Task AddAsync(string name, string login, string password);
        ApplicationUser? GetByLogin(string login);
    }
}