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
        Task<int> CreateSeasonAsync(CreateSeasonRequestDto request);
    }
}
