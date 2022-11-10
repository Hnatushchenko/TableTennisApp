using Microsoft.EntityFrameworkCore;
using TableTennisApp.Models;
using TableTennisApp.Repository;

namespace TableTennisApp.Services
{
    public class PlayersService : IPlayersService
    {
        private readonly IApplicationContext _dbContext;

        public PlayersService(IApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return _dbContext.Players.AsNoTracking();
        }

        public Player? GetByLogin(string login)
        {
            return _dbContext.Players.FirstOrDefault(p => p.Login == login);
        }

        public async Task AddAsync(string name, string login, string password)
        {
            Player player = new Player
            {
                Id = Guid.NewGuid(),
                Name = name,
                Login = login,
                Password = password,
                Rating = 1200
            };
            _dbContext.Players.Add(player);
            await _dbContext.SaveChangesAsync();
        }
    }
}
