namespace Spanish.Football.League.Common.Models
{
    /// <summary>
    /// Dto for returning all season stats for a given season.
    /// </summary>
    public class SeasonStatsResponseDto
    {
        /// <summary>
        /// Gets or sets the team name.
        /// </summary>
        public string? TeamName { get; set; }

        /// <summary>
        /// Gets or sets the scored goals.
        /// </summary>
        public int ScoredGoals { get; set; }

        /// <summary>
        /// Gets or sets the conceded goals.
        /// </summary>
        public int ConcededGoals { get; set; }

        /// <summary>
        /// Gets or sets the goal difference.
        /// </summary>
        public int GoalDifference { get; set; }

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// Gets or sets the wins.
        /// </summary>
        public int Wins { get; set; }

        /// <summary>
        /// Gets or sets the draws.
        /// </summary>
        public int Draws { get; set; }

        /// <summary>
        /// Gets or sets the losses.
        /// </summary>
        public int Losses { get; set; }
    }
}
