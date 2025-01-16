namespace Spanish.Football.League.DomainModels
{
    /// <summary>
    /// The Season entity.
    /// </summary>
    public class Season : BaseModel<int>
    {
        /// <summary>
        /// Gets or sets the season year.
        /// </summary>
        public int SeasonYear { get; set; }
    }
}
