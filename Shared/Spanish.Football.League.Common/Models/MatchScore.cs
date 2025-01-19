namespace Spanish.Football.League.Common.Models
{
    /// <summary>
    /// Represent the score for a football match for a specific teams.
    /// </summary>
    public class MatchScore
    {
        /// <summary>
        /// Gets or sets the home team score.
        /// </summary>
        public int HomeTeamScore { get; set; }

        /// <summary>
        /// Gets or sets the away team score.
        /// </summary>
        public int AwayTeamScore { get; set; }
    }
}
