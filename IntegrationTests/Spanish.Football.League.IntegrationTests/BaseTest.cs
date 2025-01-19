using Microsoft.Extensions.DependencyInjection;
using Spanish.Football.League.Database;

namespace Spanish.Football.League.IntegrationTests
{
    public class BaseTest : IClassFixture<WebAppFactory>
    {
        private readonly IServiceScope _scope;
        protected readonly SpanishFootballLeagueDbContext DbContext;

        public BaseTest(WebAppFactory factory)
        {
            _scope = factory.Services.CreateScope();
            DbContext = _scope.ServiceProvider.GetRequiredService<SpanishFootballLeagueDbContext>();
        }
    }
}
