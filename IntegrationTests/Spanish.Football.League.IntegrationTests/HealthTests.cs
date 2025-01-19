namespace Spanish.Football.League.IntegrationTests
{
    using Microsoft.EntityFrameworkCore;

    public class HealthTests(WebAppFactory factory)
        : BaseTest(factory)
    {
        private readonly HttpClient httpClient = factory.CreateDefaultClient();

        [Fact]
        public async Task HealthCheck_Returns_OK()
        {
            var response = await httpClient.GetAsync("api/healthcheck");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Check_PostgreSqlDatatabase_Responding()
        {
            var matchesCount = await DbContext.Matches.CountAsync();

            Assert.True(matchesCount == 0);
        }
    }
}
