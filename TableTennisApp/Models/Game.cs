using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TableTennisApp.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public Guid WinnerId { get; set; }
        public int WinnerScore { get; set; }
        public Guid LoserId { get; set; }
        public int LoserScore { get; set; }
        public List<ApplicationUser>? applicationUsers { get; set; }

        [NotMapped]
        public ApplicationUser? Winner => applicationUsers?.First(u => u.Id == WinnerId);

        [NotMapped]
        public ApplicationUser? Loser => applicationUsers?.First(u => u.Id == LoserId);
    }
}
