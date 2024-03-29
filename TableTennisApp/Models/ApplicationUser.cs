﻿using Microsoft.AspNetCore.Identity;

namespace TableTennisApp.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public int Rating { get; set; } = 1200;
        public int TotalNumberOfGames { get; set; }
        public List<Game>? Games { get; set; }
    }
}
