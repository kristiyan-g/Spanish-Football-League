namespace Spanish.Football.League.Services
{
    using System;
    using Spanish.Football.League.Services.Interfaces;

    /// <inheritdoc />
    public class GameEngineService : IGameEngineService
    {
        private readonly Random random = new ();

        /// <inheritdoc />
        public (int HomeScore, int AwayScore) GenerateMatchScore(decimal homeTeamWeight, decimal awayTeamWeight)
        {
            // Home team have an advantage.
            decimal adjustedHomeWeight = homeTeamWeight + 0.05m;

            int homeScore = CalculateGoals(adjustedHomeWeight);
            int awayScore = CalculateGoals(awayTeamWeight);

            return (homeScore, awayScore);
        }

        /// <summary>
        /// Calculates the number of goals scored using the Poisson distribution.
        /// </summary>
        /// <param name="teamWeight">Mean of the Poisson distribution, influenced by team weight.</param>
        /// <returns>Number of goals scored.</returns>
        private int CalculateGoals(decimal teamWeight)
        {
            // Poisson distribution's expected mean (lambda)
            decimal expectedGoals = 2.5m * teamWeight;

            // Calculate the threshold for stopping the random process
            decimal threshold = (decimal)Math.Exp((double)-expectedGoals);

            int goals = 0;
            decimal cumulativeProbability = 1.0m;

            // Generate the random probability product until it drops below the threshold
            do
            {
                goals++;
                cumulativeProbability *= (decimal)random.NextDouble();
            }
            while (cumulativeProbability > threshold);

            // Subtract 1 because the loop increments goals one extra time
            return goals - 1;
        }
    }
}
