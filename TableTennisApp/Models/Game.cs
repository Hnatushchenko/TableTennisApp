using System.ComponentModel.DataAnnotations.Schema;

namespace TableTennisApp.Models
{
    public class Game
    {
        public Guid Id { get; set; }

        public Guid WinnerId { get; set; }
        [ForeignKey("WinnerId")]
        public Player? Winner { get; set; }

        public Guid LoserId { get; set; }
        [ForeignKey("LoserId")]
        public Player? Loser { get; set; }
    }
}
