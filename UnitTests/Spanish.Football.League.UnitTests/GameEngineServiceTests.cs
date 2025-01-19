using Microsoft.Extensions.Logging;
using Spanish.Football.League.Services;
using Spanish.Football.League.Services.Interfaces;
using Spanish.Football.League.UnitTests.Utils;

namespace Spanish.Football.League.UnitTests
{
    public class GameEngineServiceTests
    {
        private readonly IGameEngineService _gameEngineService;

        public GameEngineServiceTests()
        {
            var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<GameEngineService>();
            _gameEngineService = new GameEngineService(logger);
        }

        [Fact]
        public void GenerateMatchScore_ShouldReturnValidScores()
        {
            decimal homeTeamWeight = UnitTestConstants.MaxTeamWeight;
            decimal awayTeamWeight = UnitTestConstants.MaxTeamWeight;

            var result = _gameEngineService.GenerateMatchScore(homeTeamWeight, awayTeamWeight);

            Assert.NotNull(result);
            Assert.True(result.HomeTeamScore >= UnitTestConstants.DefaultValue);
            Assert.True(result.AwayTeamScore >= UnitTestConstants.DefaultValue);
        }

        [Fact]
        public void GenerateMatchScore_HighWeightForHomeTeam_ShouldFavorHomeTeam()
        {
            decimal homeTeamWeight = UnitTestConstants.MaxTeamWeight;
            decimal awayTeamWeight = UnitTestConstants.MinTeamWeight;

            int homeTeamWins = UnitTestConstants.DefaultValue;
            int awayTeamWins = UnitTestConstants.DefaultValue;
            int totalGames = 1000; // Number of simulations to check the win percentage

            for (int i = 0; i < totalGames; i++)
            {
                var result = _gameEngineService.GenerateMatchScore(homeTeamWeight, awayTeamWeight);

                if (result.HomeTeamScore > result.AwayTeamScore)
                {
                    homeTeamWins++;
                }
                else if (result.AwayTeamScore > result.HomeTeamScore)
                {
                    awayTeamWins++;
                }
            }

            double homeWinPercentage = (double)homeTeamWins / totalGames;
            double awayWinPercentage = (double)awayTeamWins / totalGames;

            Assert.InRange(homeWinPercentage, 0.65, 1);
            Assert.InRange(awayWinPercentage, 0, 0.35);
        }


        [Fact]
        public void GenerateMatchScore_LowWeights_ShouldReturnLowScores()
        {
            decimal homeTeamWeight = UnitTestConstants.MinTeamWeight;
            decimal awayTeamWeight = UnitTestConstants.MinTeamWeight;

            var result = _gameEngineService.GenerateMatchScore(homeTeamWeight, awayTeamWeight);

            Assert.NotNull(result);
            Assert.True(result.HomeTeamScore <= 2);
            Assert.True(result.AwayTeamScore <= 2);
        }
    }
}
