namespace TableTennisApp.Models
{
    public class RatingManager : IRatingManager
    {
        public void CalculateNewRating(ApplicationUser winner, ApplicationUser loser)
        {
            double difference = loser.Rating - winner.Rating;
            double expectedScore = 1 / (1 + Math.Pow(10, difference / 400));
            winner.Rating += Convert.ToInt32(20 * (1 - expectedScore));
            loser.Rating -= Convert.ToInt32(20 * (1 - expectedScore));
        }
    }
}