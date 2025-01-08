﻿namespace Spanish.Football.League.DomainModels;

/// <summary>
/// The Winner entity.
/// </summary>
public class Winner : BaseModel<int>
{
    /// <summary>
    /// Gets or sets WinnerId.
    /// </summary>
    public int WinnerId { get; set; }

    /// <summary>
    /// Gets or sets SeasonId.
    /// </summary>
    public int SeasonId { get; set; }

    /// <summary>
    /// Gets or sets WinnerTeamName.
    /// </summary>
    public string WinnerTeamName { get; set; }

    /// <summary>
    /// Gets or sets ExpectedWinPercentage.
    /// </summary>
    public double ExpectedWinPercentage { get; set; }
}
