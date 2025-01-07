namespace Spanish.Football.League.DomainModels
{
    /// <summary>
    /// Contains the base domain model properties.
    /// </summary>
    /// <typeparam name="TKey">The template Primary Key.</typeparam>
    public abstract class BaseModel<TKey>
    {
        /// <summary>
        /// Gets or sets CreatedDate.
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
