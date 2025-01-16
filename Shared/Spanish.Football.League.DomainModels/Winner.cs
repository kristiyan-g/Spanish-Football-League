namespace Spanish.Football.League.DomainModels;

/// <summary>
/// The Winner entity.
/// </summary>
    public class Winner : BaseModel<int>
    {
        /// <summary>
        /// Gets or sets the season ID.
        /// </summary>
        public int SeasonId { get; set; }

        /// <summary>
        /// Gets or sets the winner team name.
        /// </summary>
        public string WinnerTeamName { get; set; }

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        public int Points { get; set; }
}
