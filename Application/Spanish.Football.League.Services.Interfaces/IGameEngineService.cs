namespace Spanish.Football.League.Services.Interfaces
{
    using Spanish.Football.League.Common.Models;

    /// <summary>
    /// Service that calculates match scores using the Poisson distribution.
    /// </summary>
    public interface IGameEngineService
    {
        /// <summary>
        /// Generates a random score for a match using the Poisson distribution.
        /// </summary>
        /// <param name="homeTeamWeight">Weight of the home team.</param>
        /// <param name="awayTeamWeight">Weight of the away team.</param>
        /// <returns>A tuple containing the home and away scores.</returns>
        MatchScore GenerateMatchScore(decimal homeTeamWeight, decimal awayTeamWeight);
    }
}
