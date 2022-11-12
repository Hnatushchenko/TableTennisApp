using System.ComponentModel.DataAnnotations.Schema;

namespace TableTennisApp.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        [NotMapped]
        public Player? PlayerWhoWon { get; set; }
        public Guid? PlayerWhoWonId { get; set; }
        [NotMapped]
        public Player? PlayerWhoLost { get; set; }
        public Guid? PlayerWhoLostId { get; set; }
    }
}
