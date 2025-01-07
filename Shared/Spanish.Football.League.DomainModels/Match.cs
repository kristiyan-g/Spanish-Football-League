namespace Spanish.Football.League.DomainModels;

/// <summary>
/// The Match entity.
/// </summary>
public class Match : BaseModel<int>
{
    /// <summary>
    /// Gets or sets MatchId.
    /// </summary>
    public int MatchId { get; set; }

    /// <summary>
    /// Gets or sets HomeTeamId.
    /// </summary>
    public int HomeTeamId { get; set; }

    /// <summary>
    /// Gets or sets AwayTeamId.
    /// </summary>
    public int AwayTeamId { get; set; }

    /// <summary>
    /// Gets or sets HomeTeamOdd.
    /// </summary>
    public decimal HomeTeamOdd { get; set; }

    /// <summary>
    /// Gets or sets AwayTeamOdd.
    /// </summary>
    public decimal AwayTeamOdd { get; set; }
}
