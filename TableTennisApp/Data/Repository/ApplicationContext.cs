using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Xml;
using TableTennisApp.Models;

namespace TableTennisApp.Data.Repository
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IApplicationContext
    {
        public DbSet<ApplicationUser> Players { get; set; } = null!;
        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<QueueItem> QueueItems { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Entity<QueueItem>().Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Entity<Game>().Property(p => p.Id).ValueGeneratedOnAdd();
        }
    }
}
