using Microsoft.EntityFrameworkCore;
using System.Xml;
using TableTennisApp.Models;

namespace TableTennisApp.Repository
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<QueueItem> QueueItems { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
