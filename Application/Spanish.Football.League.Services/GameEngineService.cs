namespace Spanish.Football.League.Services
{
    using System;
    using Microsoft.Extensions.Logging;
    using Spanish.Football.League.Common.Models;
    using Spanish.Football.League.Services.Interfaces;

    /// <inheritdoc />
    public class GameEngineService(ILogger<GameEngineService> logger)
        : IGameEngineService
    {
        private readonly Random random = new ();

        /// <inheritdoc />
        public MatchScore GenerateMatchScore(decimal homeTeamWeight, decimal awayTeamWeight)
        {
            logger.LogDebug($"{nameof(GenerateMatchScore)} has been invoked with params home team weight {homeTeamWeight} and away team weight {awayTeamWeight}.");

            // Home team have an advantage.
            decimal adjustedHomeWeight = homeTeamWeight + 0.05m;

            int homeTeamScore = CalculateGoals(adjustedHomeWeight);
            int awayTeamScore = CalculateGoals(awayTeamWeight);

            return new MatchScore
            {
                HomeTeamScore = homeTeamScore,
                AwayTeamScore = awayTeamScore,
            };
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
