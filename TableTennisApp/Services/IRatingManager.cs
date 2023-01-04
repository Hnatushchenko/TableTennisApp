namespace TableTennisApp.Models
{
    public interface IRatingManager
    {
        void CalculateNewRating(ApplicationUser playerWhoWon, ApplicationUser playerWhoLost);
    }
}