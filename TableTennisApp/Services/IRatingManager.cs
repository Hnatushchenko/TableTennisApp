namespace TableTennisApp.Models
{
    public interface IRatingManager
    {
        void CalculateNewRating(Player playerWhoWon, Player playerWhoLost);
    }
}