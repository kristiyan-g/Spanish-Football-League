namespace Spanish.Football.League.Common.Models
{
    /// <summary>
    /// Dto for returning information for a given season.
    /// </summary>
    public class SeasonResultsResponseDto
    {
        /// <summary>
        /// Gets or sets the season year.
        /// </summary>
        public int SeasonYear { get; set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public IEnumerable<ResultResponseDto>? Results { get; set; }

        /// <summary>
        /// Gets or sets the winner.
        /// </summary>
        public WinnerResponseDto? Winner { get; set; }
    }
}
