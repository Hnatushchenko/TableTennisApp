using Microsoft.EntityFrameworkCore;
using TableTennisApp.Models;
using TableTennisApp.Repository;
using TableTennisApp.Exceptions;

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
            return _dbContext.Players.SingleOrDefault(p => p.Login == login);
        }

        public async Task AddAsync(string name, string login, string password)
        {
            Player? playerInDb = GetByLogin(login);
            if (playerInDb is not null)
            {
                throw new PlayerAlreadyExistsException();
            }

            Player newPlayer = new Player
            {
                Id = Guid.NewGuid(),
                Name = name,
                Login = login,
                Password = password,
                Rating = 1200
            };
            _dbContext.Players.Add(newPlayer);
            await _dbContext.SaveChangesAsync();
        }
    }
}
