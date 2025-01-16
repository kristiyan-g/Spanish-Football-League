namespace Spanish.Football.League.Services.Utils
{
    /// <summary>
    /// A utility class for generating random weight values.
    /// This class contains a method to generate a random weight value within a specified range.
    /// </summary>
    public static class RandomWeightGenerator
    {
        private static readonly Random Random = new Random();

        /// <summary>
        /// Generates a random weight value between a specified minimum and maximum range.
        /// This method produces a random decimal number within the provided range and rounds it to two decimal places.
        /// </summary>
        /// <param name="min">The minimum weight value that can be generated.</param>
        /// <param name="max">The maximum weight value that can be generated.</param>
        /// <returns>A decimal value representing the randomly generated weight within the specified range.</returns>
        public static decimal GenerateRandomWeight(decimal min, decimal max)
        {
            double randomValue = Random.NextDouble();

            decimal weight = (decimal)((randomValue * ((double)max - (double)min)) + (double)min);

            return Math.Round(weight, 2);
        }
    }
}
