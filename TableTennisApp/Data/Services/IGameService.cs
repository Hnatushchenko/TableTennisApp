using TableTennisApp.Data.ViewModels;
using TableTennisApp.Models;

namespace TableTennisApp.Services
{
    public interface IGameService
    {
        Task AddAsync(GameVM gameVM);
        Task<IEnumerable<Game>> GetAllGamesAsync();
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Game>> GetAllGamesByUserIdAsync(Guid userId);
    }
}