namespace Spanish.Football.League.Common.Models
{
    /// <summary>
    /// Dto for returning all team details for a given season.
    /// </summary>
    public class TeamDetailsResponseDto
    {
        /// <summary>
        /// Gets or sets the team name.
        /// </summary>
        public string? TeamName { get; set; }

        /// <summary>
        /// Gets or sets the team color.
        /// </summary>
        public string? TeamColor { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// Gets or sets the expected percentage for win.
        /// </summary>
        public int ExpectedWinPercentage { get; set; }
    }
}
