namespace Spanish.Football.League.Common.Models
{
    /// <summary>
    /// Dto for creating Team details.
    /// </summary>
    public class CreateTeamDetailsDto
    {
        /// <summary>
        /// Gets or sets the season ID.
        /// </summary>
        public int SeasonId { get; set; }

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
        /// Gets or sets the expexted percents to win.
        /// </summary>
        public decimal ExpectedWinPercentage { get; set; }
    }
}
