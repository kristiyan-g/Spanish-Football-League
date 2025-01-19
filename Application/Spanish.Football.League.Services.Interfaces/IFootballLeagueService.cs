namespace Spanish.Football.League.Services.Interfaces
{
    using Spanish.Football.League.Common.Models;

    /// <summary>
    /// Responsible for create season game.
    /// </summary>
    public interface IFootballLeagueService
    {
        /// <summary>
        /// Creates a new football season with the specified parameters.
        /// </summary>
        /// <param name="request">The request object containing season parameters.</param>
        /// <returns>The ID of the newly created season.</returns>
        Task<int?> CreateSeasonAsync(CreateSeasonRequestDto request);

        /// <summary>
        /// Retrieves the results of a specific season, including the season details, match results, and the season winner.
        /// </summary>
        /// <param name="seasonId">The ID of the season for which the results are requested.</param>
        /// <returns>
        /// A <see cref="SeasonResultsResponseDto"/> containing the season details, match results, and winner if found,
        /// or null if no results are found for the specified season ID.
        /// </returns>
        Task<SeasonResultsResponseDto?> GetSeasonResultsAsync(int seasonId);

        /// <summary>
        /// Retrieves the leaderboard stats for a specific season, ordered by points in descending order.
        /// </summary>
        /// <param name="seasonId">The ID of the season for which the stats are requested.</param>
        /// <returns>
        /// An <see cref="IEnumerable{SeasonStatsResponseDto}"/> containing the season's leaderboard stats,
        /// or null if no stats are found for the specified season ID.
        /// </returns>
        Task<IEnumerable<SeasonStatsResponseDto>?> GetSeasonStatsAsync(int seasonId);

        /// <summary>
        /// Retrieves the details of the teams for a specific season, ordered by expected win percentage in descending order.
        /// </summary>
        /// <param name="seasonId">The ID of the season for which the team details are requested.</param>
        /// <returns>
        /// An <see cref="IEnumerable{TeamDetailsResponseDto}"/> containing the details of the teams for the specified season,
        /// or null if no team details are found for the given season ID.
        /// </returns>
        Task<IEnumerable<TeamDetailsResponseDto>?> GetTeamDetailsAsync(int seasonId);
    }
}
