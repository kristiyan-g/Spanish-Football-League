namespace Spanish.Football.League.Services.Utils
{
    using Spanish.Football.League.Common;
    using Spanish.Football.League.Common.Models;

    /// <summary>
    /// A utility class that calculates the odds for a match between two teams based on their strengths.
    /// It considers home team advantage and ensures that the calculated odds fall within a specified range.
    /// </summary>
    public static class OddsCalculator
    {
        /// <summary>
        /// Calculates the odds for a match between two teams based on their weights (strengths).
        /// The odds are adjusted for home team advantage, and they are clamped to a specified minimum and maximum range.
        /// </summary>
        /// <param name="homeTeamWeight">The weight (strength) of the home team, which is adjusted for home advantage.</param>
        /// <param name="awayTeamWeight">The weight (strength) of the away team.</param>
        /// <returns>A <see cref="TeamsOdds"/> object containing the calculated odds for both the home and away teams.</returns>
        public static TeamsOdds CalculateOdds(decimal homeTeamWeight, decimal awayTeamWeight)
        {
            // Adjust home weight for home team advantage.
            decimal adjustedHomeTeamWeight = homeTeamWeight + GameConstants.HomeTeamAdvantage;

            // Calculate total weight
            decimal totalWeight = adjustedHomeTeamWeight + awayTeamWeight;

            // Calculate probabilities
            decimal homeProbability = adjustedHomeTeamWeight / totalWeight;
            decimal awayProbability = awayTeamWeight / totalWeight;

            // Calculate odds
            decimal homeOdds = 1 / homeProbability;
            decimal awayOdds = 1 / awayProbability;

            // Clamp the odds to a defined range
            const decimal MinOdds = 1.10m;
            const decimal MaxOdds = 7.00m;

            homeOdds = Math.Clamp(homeOdds, MinOdds, MaxOdds);
            awayOdds = Math.Clamp(awayOdds, MinOdds, MaxOdds);

            // Return the calculated odds, rounded to two decimal places
            var teamsOdds = new TeamsOdds
            {
                HomeTeamOdd = Math.Round(homeOdds, 2),
                AwayTeamOdd = Math.Round(awayOdds, 2),
            };

            return teamsOdds;
        }
    }
}
