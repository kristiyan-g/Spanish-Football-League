namespace Spanish.Football.League.DomainModels;

using Spanish.Football.League.Common.Enums;

/// <summary>
/// The Result entity.
/// </summary>
public class Result : BaseModel<int>
{
    /// <summary>
    /// Gets or sets the season ID.
    /// </summary>
    public int SeasonId { get; set; }

    /// <summary>
    /// Gets or sets the season half.
    /// </summary>
    public SeasonHalvesEnum SeasonHalf { get; set; }

    /// <summary>
    /// Gets or sets the home team name.
    /// </summary>
    public string HomeTeamName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the home team odd.
    /// </summary>
    public decimal HomeTeamOdd { get; set; }

    /// <summary>
    /// Gets or sets the away team name.
    /// </summary>
    public string AwayTeamName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the away team odd.
    /// </summary>
    public decimal AwayTeamOdd { get; set; }

    /// <summary>
    /// Gets or sets the home team score.
    /// </summary>
    public int HomeTeamScore { get; set; }

    /// <summary>
    /// Gets or sets the away team score.
    /// </summary>
    public int AwayTeamScore { get; set; }

    /// <summary>
    /// Gets or sets is expected.
    /// </summary>
    public bool? IsExpected { get; set; }
}
