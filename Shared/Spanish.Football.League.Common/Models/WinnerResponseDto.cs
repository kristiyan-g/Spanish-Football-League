namespace Spanish.Football.League.Common.Models
{
    /// <summary>
    /// Dto for returning winner for a given season.
    /// </summary>
    public class WinnerResponseDto
    {
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
