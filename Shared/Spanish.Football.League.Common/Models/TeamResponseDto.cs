namespace Spanish.Football.League.Common.Models
{
    /// <summary>
    /// Dto to returning teams to client.
    /// </summary>
    public class TeamResponseDto
    {
        /// <summary>
        /// Gets or sets the name of the team.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the color of the team.
        /// </summary>
        public string? Color { get; set; }
    }
}
