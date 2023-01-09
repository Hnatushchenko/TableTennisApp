using Microsoft.EntityFrameworkCore;
using TableTennisApp.Data.ViewModels;
using TableTennisApp.Models;
using TableTennisApp.Repository;

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
    }
}
