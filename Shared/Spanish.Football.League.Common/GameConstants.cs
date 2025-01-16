namespace Spanish.Football.League.Common
{
    /// <summary>
    /// A class that holds constants for the game.
    /// </summary>
    public class GameConstants
    {
        /// <summary>
        /// The minimum weight for strong teams..
        /// </summary>
        public const decimal StrongTeamsMinWeight = 0.8m;

        /// <summary>
        /// The maximum weight for strong teams.
        /// </summary>
        public const decimal StrongTeamsMaxWeight = 1;

        /// <summary>
        /// The minimum weight for weak teams.
        /// </summary>
        public const decimal WeakTeamsMinWeight = 0.01m;

        /// <summary>
        /// The maximum weight for weak teams.
        /// </summary>
        public const decimal WeakTeamsMaxWeight = 0.2m;

        /// <summary>
        /// The advantage for the home team.
        /// </summary>
        public const decimal HomeTeamAdvantage = 0.05m;
    }
}
