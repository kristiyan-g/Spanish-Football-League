namespace Spanish.Football.League.Common.Models
{
    /// <summary>
    /// Dto for creating Teams with random weight.
    /// </summary>
    public class TeamDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        public string? Color { get; set; }
    }
}
