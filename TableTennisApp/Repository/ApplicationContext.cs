﻿using Microsoft.EntityFrameworkCore;
using System.Xml;
using TableTennisApp.Models;

namespace TableTennisApp.Repository
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<Game> Games { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SaveChangesAsync();
            modelBuilder.Entity<Player>().HasData(
                new Player
                {
                    Id = Guid.NewGuid(),
                    Name = "Andrii Hnatushchenko",
                    Rating = 1200
                });
        }
    }
}
