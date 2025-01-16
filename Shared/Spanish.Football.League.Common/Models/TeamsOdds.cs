namespace Spanish.Football.League.Common.Models
{
    /// <summary>
    /// Represents the odds for a football match for a specific teams.
    /// </summary>
    public class TeamsOdds
    {
        /// <summary>
        /// Gets or sets the odd for the home team to win.
        /// </summary>
        public decimal HomeTeamOdd { get; set; }

        /// <summary>
        /// Gets or sets the odd for the away team to win.
        /// </summary>
        public decimal AwayTeamOdd { get; set; }
    }
}
