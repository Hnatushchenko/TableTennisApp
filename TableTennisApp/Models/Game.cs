using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TableTennisApp.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public int WinnerScore { get; set; }
        public int LoserScore { get; set; }
        [JsonIgnore]
        public List<ApplicationUser>? applicationUsers { get; set; }

        [NotMapped] 
        public Guid? WinnerId  => applicationUsers?.FirstOrDefault()?.Id;
        [JsonIgnore]
        [NotMapped]
        public ApplicationUser? Winner => applicationUsers?.FirstOrDefault();

        [NotMapped]
        public Guid? LoserId => applicationUsers?.LastOrDefault()?.Id;
        [JsonIgnore]
        [NotMapped]
        public ApplicationUser? Loser => applicationUsers?.LastOrDefault();


    }
}
