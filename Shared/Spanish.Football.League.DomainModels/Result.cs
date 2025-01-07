namespace Spanish.Football.League.DomainModels;

/// <summary>
/// The Result entity.
/// </summary>
public class Result : BaseModel<int>
{
    /// <summary>
    /// Gets or sets Result Id.
    /// </summary>
    public int ResultId { get; set; }

    /// <summary>
    /// Gets or sets HomeTeamId.
    /// </summary>
    public int HomeTeamId { get; set; }

    /// <summary>
    /// Gets or sets AwayTeamId.
    /// </summary>
    public int AwayTeamId { get; set; }

    /// <summary>
    /// Gets or sets Score.
    /// </summary>
    public string? Score { get; set; }
}
