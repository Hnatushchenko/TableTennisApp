namespace TableTennisApp.Models
{
    public interface IRatingManager
    {
        void CalculateNewRating(ApplicationUser winner, ApplicationUser loser);
    }
}