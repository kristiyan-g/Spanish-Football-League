namespace Spanish.Football.League.Common.Models
{
    using Spanish.Football.League.Common.Enums;

    /// <summary>
    /// Dto for creating matches.
    /// </summary>
    public class CreateMatchDto
    {
        /// <summary>
        /// Gets or sets the season ID.
        /// </summary>
        public int SeasonId { get; set; }

        /// <summary>
        /// Gets or sets the season half (Spring, Fall).
        /// </summary>
        public SeasonHalvesEnum SeasonHalf { get; set; }

        /// <summary>
        /// Gets or sets the home team name.
        /// </summary>
        public string? HomeTeamName { get; set; }

        /// <summary>
        /// Gets or sets the away team name.
        /// </summary>
        public string? AwayTeamName { get; set; }

        /// <summary>
        /// Gets or sets the home team odd.
        /// </summary>
        public decimal HomeTeamOdd { get; set; }

        /// <summary>
        /// Gets or sets the away team odd.
        /// </summary>
        public decimal AwayTeamOdd { get; set; }
    }
}
