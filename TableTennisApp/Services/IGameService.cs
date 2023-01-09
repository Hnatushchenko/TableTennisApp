using TableTennisApp.Data.ViewModels;
using TableTennisApp.Models;

namespace TableTennisApp.Services
{
    public interface IGameService
    {
        Task AddAsync(GameVM gameVM);
        Task<IEnumerable<Game>> GetAllGamesAsync();
    }
}