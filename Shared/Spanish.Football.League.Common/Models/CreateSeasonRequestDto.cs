namespace Spanish.Football.League.Common.Models
{
    /// <summary>
    /// Represents a data transfer object (DTO) for creating a new football season.
    /// </summary>
    public class CreateSeasonRequestDto
    {
        /// <summary>
        /// Gets or sets the year of the season to be created.
        /// </summary>
        public int SeasonYear { get; set; }

        /// <summary>
        /// Gets or sets the total number of teams participating in the season.
        /// </summary>
        public int NumberOfTeams { get; set; }

        /// <summary>
        /// Gets or sets the number of strong teams to include in the season.
        /// </summary>
        public int NumberOfStrongTeams { get; set; }

        /// <summary>
        /// Gets or sets the number of weak teams to include in the season.
        /// </summary>
        public int NumberOfWeakTeams { get; set; }
    }
}
