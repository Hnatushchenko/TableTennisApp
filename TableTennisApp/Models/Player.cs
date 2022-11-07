﻿namespace TableTennisApp.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Rating { get; set; }
        public int TotalGames { get; set; }
        public List<Game>? Games { get; set; }
    }
}
