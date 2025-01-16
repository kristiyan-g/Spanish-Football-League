namespace Spanish.Football.League.DomainModels
{
    /// <summary>
    /// The TeamDetails entity.
    /// </summary>
    public class TeamDetails : BaseModel<int>
    {
        /// <summary>
        /// Gets or sets the seaaon ID.
        /// </summary>
        public int SeasonId { get; set; }

        /// <summary>
        /// Gets or sets the team name.
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// Gets or sets the expexted perentage to win.
        /// </summary>
        public decimal ExpectedWinPercentage { get; set; }
    }
}
