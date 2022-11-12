using Microsoft.EntityFrameworkCore;
using TableTennisApp.Models;
using TableTennisApp.Repository;

namespace TableTennisApp.Services
{
    public class GameService : IGameService
    {
        private readonly IApplicationContext _dbContext;
        private readonly IRatingManager _ratingManager;

        public GameService(IApplicationContext dbContext, IRatingManager ratingManager)
        {
            _dbContext = dbContext;
            _ratingManager = ratingManager;
        }

        public async Task AddAsync(string playerWhoWonLogin, string playerWhoLostLogin)
        {
            Player playerWhoWon = _dbContext.Players.Single(p => p.Login == playerWhoWonLogin);
            Player playerWhoLost = _dbContext.Players.Single(p => p.Login == playerWhoLostLogin);
            _ratingManager.CalculateNewRating(playerWhoWon, playerWhoLost);
            playerWhoLost.TotalGames++;
            playerWhoWon.TotalGames++;
            Game game = new Game
            {
                Id = Guid.NewGuid(),
                PlayerWhoWonId = playerWhoWon.Id,
                PlayerWhoLostId = playerWhoLost.Id,
            };
            _dbContext.Games.Add(game);
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<Game> GetAllGames()
        {
            var games = _dbContext.Games.AsNoTracking();
            return games;
        }
    }
}
