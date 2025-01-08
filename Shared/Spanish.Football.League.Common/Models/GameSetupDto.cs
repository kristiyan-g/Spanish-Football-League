namespace Spanish.Football.League.Common.Models
{
    public class GameSetupDto
    {
        public int NumberOfTeams { get; set; }

        public int NumberOfStrongTeams { get; set; }

        public decimal StrongTeamsMin { get; set; }

        public decimal StrongTeamsMax { get; set; }

        public int NumberOfWeakTeams { get; set; }

        public decimal WeakTeamsMin { get; set; }

        public decimal WeakTeamsMax { get; set; }
    }
}
