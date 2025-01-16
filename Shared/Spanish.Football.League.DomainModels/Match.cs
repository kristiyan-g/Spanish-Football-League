namespace Spanish.Football.League.DomainModels;

using Spanish.Football.League.Common.Enums;

/// <summary>
/// The Match entity.
/// </summary>
public class Match : BaseModel<int>
{
    /// <summary>
    /// Gets or sets the Season ID.
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
}
