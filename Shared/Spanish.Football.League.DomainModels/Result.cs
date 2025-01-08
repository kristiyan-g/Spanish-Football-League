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
    /// Gets or sets HomeTeamName.
    /// </summary>
    public string HomeTeamName { get; set; }

    /// <summary>
    /// Gets or sets AwayTeamName.
    /// </summary>
    public string AwayTeamName { get; set; }

    /// <summary>
    /// Gets or sets Score.
    /// </summary>
    public string? Score { get; set; }
}
