using TableTennisApp.Repository;

namespace TableTennisApp.Models
{
    public class RatingManager : IRatingManager
    {
        public void CalculateNewRating(ApplicationUser playerWhoWon, ApplicationUser playerWhoLost)
        {
            double difference = playerWhoLost.Rating - playerWhoWon.Rating;
            double expectedScore = 1 / (1 + Math.Pow(10, difference / 400));
            playerWhoWon.Rating += Convert.ToInt32(20 * (1 - expectedScore));
            playerWhoLost.Rating -= Convert.ToInt32(20 * (1 - expectedScore));
        }
    }
}