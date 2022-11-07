using Microsoft.EntityFrameworkCore;
using TableTennisApp.Models;

namespace TableTennisApp.Repository
{
    public interface IApplicationContext
    {
        DbSet<Game> Games { get; set; }
        DbSet<Player> Players { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}