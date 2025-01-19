using Spanish.Football.League.Services.Utils;
using Spanish.Football.League.UnitTests.Utils;

namespace Spanish.Football.League.UnitTests
{
    public class RandomWeightGeneratorTests
    {
        [Fact]
        public void GenerateRandomWeight_ShouldReturnValueWithinRange()
        {
            decimal min = UnitTestConstants.MinTeamWeight;
            decimal max = UnitTestConstants.MaxTeamWeight;

            decimal result = RandomWeightGenerator.GenerateRandomWeight(min, max);

            Assert.InRange(result, min, max);
        }

        [Fact]
        public void GenerateRandomWeight_ShouldReturnValueWithTwoDecimalPlaces()
        {
            decimal min = UnitTestConstants.MinTeamWeight;
            decimal max = UnitTestConstants.MaxTeamWeight;

            decimal result = RandomWeightGenerator.GenerateRandomWeight(min, max);

            Assert.Equal(result, Math.Round(result, 2));
        }

        [Fact]
        public void GenerateRandomWeight_ShouldHandleEdgeCases()
        {
            decimal min = UnitTestConstants.MinTeamWeight;
            decimal max = UnitTestConstants.MinTeamWeight;

            decimal result = RandomWeightGenerator.GenerateRandomWeight(min, max);

            Assert.Equal(min, result);

            min = UnitTestConstants.MinTeamWeight;
            max = UnitTestConstants.MaxTeamWeight;
            result = RandomWeightGenerator.GenerateRandomWeight(min, max);

            Assert.InRange(result, min, max);
        }
    }
}
