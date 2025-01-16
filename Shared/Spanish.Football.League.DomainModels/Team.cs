namespace Spanish.Football.League.DomainModels;

/// <summary>
/// The Team entity.
/// </summary>
public class Team
{
    /// <summary>
    /// Gets or sets teamId.
    /// </summary>
    public int TeamId { get; set; }

    /// <summary>
    /// Gets or sets name.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets weight.
    /// </summary>
    public decimal Weight { get; set; }

    /// <summary>
    /// Gets or sets color.
    /// </summary>
    public string Color { get; set; } = null!;
}
