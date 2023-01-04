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

        public IEnumerable<ApplicationUser> GetAllPlayers()
        {
            return _dbContext.Players.AsNoTracking();
        }

        public ApplicationUser? GetByLogin(string login)
        {
            return _dbContext.Players.SingleOrDefault(p => p.Email == login);
        }

        public async Task AddAsync(string name, string login, string password)
        {
            ApplicationUser? playerInDb = GetByLogin(login);
            if (playerInDb is not null)
            {
                throw new PlayerAlreadyExistsException();
            }

            ApplicationUser newPlayer = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = name,
                Email = login,
                 
                Rating = 1200
            };
            _dbContext.Players.Add(newPlayer);
            await _dbContext.SaveChangesAsync();
        }
    }
}
