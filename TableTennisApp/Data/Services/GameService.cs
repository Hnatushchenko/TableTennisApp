using Microsoft.EntityFrameworkCore;
using TableTennisApp.Data.ViewModels;
using TableTennisApp.Models;
using TableTennisApp.Data.Repository;

namespace TableTennisApp.Services
{
    public class GameService : IGameService
    {
        private readonly IApplicationContext _dbContext;
        private readonly IRatingManager _ratingManager;
        private readonly ApplicationUserManager _userManager;

        public GameService(IApplicationContext dbContext, IRatingManager ratingManager, ApplicationUserManager userManager)
        {
            _dbContext = dbContext;
            _ratingManager = ratingManager;
            _userManager = userManager;
        }

        public async Task AddAsync(GameVM gameVM)
        {
            var winner = await _userManager.FindByIdAsync(gameVM.WinnerId.ToString());
            var loser = await _userManager.FindByIdAsync(gameVM.LoserId.ToString());
            _ratingManager.CalculateNewRating(winner, loser);
            winner.TotalNumberOfGames++;
            loser.TotalNumberOfGames++;
            Game game = new Game
            {
                WinnerId = gameVM.WinnerId,
                LoserId = gameVM.LoserId,
                WinnerScore = gameVM.WinnerScore,
                LoserScore = gameVM.LoserScore,
                applicationUsers = new List<ApplicationUser>() { winner, loser }
            };
            _dbContext.Games.Add(game);
            await _userManager.UpdateAsync(winner);
            await _userManager.UpdateAsync(loser);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Game>> GetAllGamesAsync()
        {
            var games = await _dbContext.Games
                .Include(game => game.applicationUsers)
                .AsNoTracking()
                .ToListAsync();
            return games;
        }

        public async Task<IEnumerable<Game>> GetAllGamesByUserIdAsync(Guid userId)
        {
            var games = await _dbContext.Games
                .Where(game => game.WinnerId == userId || game.LoserId == userId)
                .Include(game => game.applicationUsers)
                .AsNoTracking()
                .ToListAsync();
            return games;
        }

        public async Task DeleteAsync(Guid id)
        {
            var game = await _dbContext.Games
                .Include(g => g.applicationUsers)
                .FirstAsync(game => game.Id == id);

            if (game is null || game.applicationUsers is null) return;

            foreach (var user in game.applicationUsers)
            {
                user.TotalNumberOfGames--;
            }
            _dbContext.Games.Remove(game);
            await _dbContext.SaveChangesAsync();
        }
    }
}
