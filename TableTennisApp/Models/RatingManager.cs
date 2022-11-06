namespace TableTennisApp.Models
{
    public class RatingManager
    {
        public void CalculateNewRating(Player player1, Player player2, int scoreDifference)
        {
            double K = scoreDifference switch
            {
                < 0 => -1,
                0 => 0.5,
                > 0 => 1
            };
            double difference = Math.Abs(player2.Rating - player1.Rating);
            double expectedScore = 1 / (1 + Math.Pow(10, difference / 400));
            double ratio = K switch
            {
                -1 => -1 + expectedScore,
                0.5 => 0.5 - expectedScore,
                1 => 1 - expectedScore,
                _ => throw new Exception($"K cannot be equal to {K}")
            };
            if (scoreDifference == 0 && player1.Rating > player2.Rating)
            {
                (player1, player2) = (player2, player1);
            }
            player1.Rating += Convert.ToInt32(20 * ratio);
            player2.Rating -= Convert.ToInt32(20 * ratio);
        }
    }
}