using System.ComponentModel.DataAnnotations.Schema;

namespace TableTennisApp.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        [NotMapped]
        public Player? Player1 { get; set; }
        public Guid? Player1Id { get; set; }
        [NotMapped]
        public Player? Player2 { get; set; }
        public Guid? Player2Id { get; set; }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;
    }
}
