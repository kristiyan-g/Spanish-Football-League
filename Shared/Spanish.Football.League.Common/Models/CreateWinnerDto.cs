namespace Spanish.Football.League.Common.Models
{
    /// <summary>
    /// Dto for creating Winner.
    /// </summary>
    public class CreateWinnerDto
    {
        /// <summary>
        /// Gets or sets the season ID.
        /// </summary>
        public int SeasonId { get; set; }

        /// <summary>
        /// Gets or sets the winner team name.
        /// </summary>
        public string? WinnerTeamName { get; set; }

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        public int Points { get; set; }
    }
}
