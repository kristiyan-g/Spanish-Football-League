namespace Spanish.Football.League.DomainModels;

/// <summary>
/// The SeasonStats entity.
/// </summary>
public class SeasonStats : BaseModel<int>
{
    // Gets or sets season ID.
    public int SeasonId { get; set; }

    /// <summary>
    /// Gets or sets team name.
    /// </summary>
    public string TeamName { get; set; }

    /// <summary>
    /// Gets or sets scored goals.
    /// </summary>
    public int ScoredGoals { get; set; }

    /// <summary>
    /// Gets or sets conceded goals.
    /// </summary>
    public int ConcededGoals { get; set; }

    /// <summary>
    /// Gets or sets goal the goal difference.
    /// </summary>
    public int GoalDifference { get; set; }

    /// <summary>
    /// Gets or sets points.
    /// </summary>
    public int Points { get; set; }

    /// <summary>
    /// Gets or sets wins.
    /// </summary>
    public int Wins { get; set; }

    /// <summary>
    /// Gets or sets draws.
    /// </summary>
    public int Draws { get; set; }

    /// <summary>
    /// Gets or sets losses.
    /// </summary>
    public int Losses { get; set; }
}