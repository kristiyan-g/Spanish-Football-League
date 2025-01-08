namespace Spanish.Football.League.Services
{
    using Spanish.Football.League.Common.Models;
    using Spanish.Football.League.DomainModels;
    using Spanish.Football.League.Repository;
    using Spanish.Football.League.Services.Mappers;

    public class GameEngineService(MapperlyProfile mapperly,
        IGenericRepository<Team, int> repository)
    {
        private readonly Random _random = new Random();

        public IEnumerable<TeamDto> GenerateTeams(GameSetupDto gameSetup)
        {
            var teams = repository.GetAll().Take(gameSetup.NumberOfTeams);

            var teamsDto = mapperly.MapToTeamDtoList(teams).ToList();

            for (int i = 0; i < gameSetup.NumberOfWeakTeams; i++)
            {
                teamsDto[i].Weight = GenerateRandomWeight(gameSetup.WeakTeamsMin, gameSetup.WeakTeamsMax);
            }

            for (int i = gameSetup.NumberOfWeakTeams; i < gameSetup.NumberOfWeakTeams + gameSetup.NumberOfStrongTeams; i++)
            {
                teamsDto[i].Weight = GenerateRandomWeight(gameSetup.StrongTeamsMin, gameSetup.StrongTeamsMax);
            }

            decimal otherTeamsMin = gameSetup.StrongTeamsMin;
            decimal otherTeamsMax = gameSetup.WeakTeamsMax;
            for (int i = gameSetup.NumberOfWeakTeams + gameSetup.NumberOfStrongTeams; i < teamsDto.Count; i++)
            {
                teamsDto[i].Weight = GenerateRandomWeight(otherTeamsMin, otherTeamsMax);
            }

            return teamsDto;
        }

        public IEnumerable<Match> GenerateMatches(List<TeamDto> teamDto)
        {

            //var teams = context.Teams.ToList();
            // teamDto = teamDto.ToList();
            var random = new Random();
            var matches = new List<Match>();

            // Generate matches
            for (int i = 0; i < teamDto.Count; i += 2)
            {
                var team1 = teamDto[i];
                var team2 = teamDto[i + 1];

                // Home and away matches
                matches.Add(new Match
                {
                    HomeTeamName = team1.Name,
                    AwayTeamName = team2.Name,
                    HomeTeamOdd = Math.Round((decimal)(random.NextDouble() * 3 + 1), 2), // Random odds
                    AwayTeamOdd = Math.Round((decimal)(random.NextDouble() * 3 + 1), 2),
                    CreatedDate = DateTime.UtcNow
                });

                matches.Add(new Match
                {
                    HomeTeamName = team2.Name,
                    AwayTeamName = team1.Name,
                    HomeTeamOdd = Math.Round((decimal)(random.NextDouble() * 3 + 1), 2), // Random odds
                    AwayTeamOdd = Math.Round((decimal)(random.NextDouble() * 3 + 1), 2),
                    CreatedDate = DateTime.UtcNow
                });
            }

            // Shuffle matches randomly
           // matches = matches.OrderBy(x => random.Next()).ToList();

            return matches;
        }

        private int GenerateScore(int min, int max, decimal strength, bool isHome)
        {
            // Apply a bias based on the team's strength
            double strengthFactor = (double)strength + (isHome ? 0.1 : 0); // Home team gets a slight boost
            int weightedMax = (int)(max * strengthFactor);

            // Generate score within the weighted range
            return _random.Next(min, Math.Max(weightedMax, min) + 1);
        }

        private decimal GenerateRandomWeight(decimal min, decimal max)
        {
            var randomValue = (decimal)(_random.NextDouble() * (double)(max - min) + (double)min);
            return Math.Round(randomValue, 2);
        }
    }
}