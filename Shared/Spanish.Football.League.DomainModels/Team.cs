namespace Spanish.Football.League.DomainModels;

/// <summary>
/// The Team entity.
/// </summary>
public class Team
{
    /// <summary>
    /// Gets or sets TeamId.
    /// </summary>
    public int TeamId { get; set; }

    /// <summary>
    /// Gets or sets Name.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets Weight.
    /// </summary>
    public double Weight { get; set; }

    /// <summary>
    /// Gets or sets Color.
    /// </summary>
    public string Color { get; set; } = null!;
}
