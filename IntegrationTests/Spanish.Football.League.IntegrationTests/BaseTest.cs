namespace Spanish.Football.League.IntegrationTests
{
    using Microsoft.Extensions.DependencyInjection;
    using Spanish.Football.League.Database;

    public class BaseTest : IClassFixture<WebAppFactory>
    {
        private readonly IServiceScope scope;
        protected readonly SpanishFootballLeagueDbContext DbContext;

        public BaseTest(WebAppFactory factory)
        {
            scope = factory.Services.CreateScope();
            DbContext = scope.ServiceProvider.GetRequiredService<SpanishFootballLeagueDbContext>();
        }
    }
}
