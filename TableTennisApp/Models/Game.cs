using System.ComponentModel.DataAnnotations.Schema;

namespace TableTennisApp.Models
{
    public class Game
    {
        public Guid Id { get; set; }

        public Guid WinnerId { get; set; }
        [NotMapped]
        public ApplicationUser? Winner { get; set; }

        public Guid LoserId { get; set; }
        [NotMapped]
        public ApplicationUser? Loser { get; set; }
    }
}
