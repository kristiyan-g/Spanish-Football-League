using Spanish.Football.League.Services.Utils;
using Spanish.Football.League.UnitTests.Utils;

namespace Spanish.Football.League.UnitTests
{
    public class OddsCalculatorTests
    {
        [Fact]
        public void CalculateOdds_ShouldReturnCorrectOdds_WhenValidWeightsProvided()
        {
            decimal homeTeamWeight = UnitTestConstants.MaxTeamWeight;
            decimal awayTeamWeight = UnitTestConstants.MinTeamWeight;

            var result = OddsCalculator.CalculateOdds(homeTeamWeight, awayTeamWeight);

            Assert.NotNull(result);
            Assert.True(result.HomeTeamOdd < result.AwayTeamOdd);
        }

        [Fact]
        public void CalculateOdds_HomeTeamAdvantage_ShouldAdjustHomeOdds()
        {
            decimal homeTeamWeight = UnitTestConstants.MaxTeamWeight;
            decimal awayTeamWeight = UnitTestConstants.MaxTeamWeight;

            var result = OddsCalculator.CalculateOdds(homeTeamWeight, awayTeamWeight);

            Assert.True(result.HomeTeamOdd < result.AwayTeamOdd);
        }

        [Fact]
        public void CalculateOdds_ShouldClampOddsToMinAndMaxRange()
        {
            decimal homeTeamWeight = UnitTestConstants.MaxTeamWeight;
            decimal awayTeamWeight = 0.1m;

            var result = OddsCalculator.CalculateOdds(homeTeamWeight, awayTeamWeight);

            Assert.True(result.HomeTeamOdd >= 1.10m && result.HomeTeamOdd <= 7.00m);
            Assert.True(result.AwayTeamOdd >= 1.10m && result.AwayTeamOdd <= 7.00m);
        }

        [Fact]
        public void CalculateOdds_ShouldBeProper()
        {
            decimal homeTeamWeight = UnitTestConstants.MaxTeamWeight;
            decimal awayTeamWeight = UnitTestConstants.MinTeamWeight;

            var result = OddsCalculator.CalculateOdds(homeTeamWeight, awayTeamWeight);

            Assert.True(result.HomeTeamOdd == 1.10m);
            Assert.True(result.AwayTeamOdd == 7.0m);
        }
    }
}
