namespace TableTennisApp.Models
{
    public class RatingManager
    {
        public static void RatingCount(Player PlayerWhoWon, Player PlayerWhoLost)
        {
            PlayerWhoWon.Rating += 30;
            PlayerWhoLost.Rating -= 27;
        }
    }
}