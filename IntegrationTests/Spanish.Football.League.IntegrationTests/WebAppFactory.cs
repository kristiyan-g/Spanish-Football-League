using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Spanish.Football.League.Database;
using Testcontainers.PostgreSql;
using Testcontainers.Redis;

namespace Spanish.Football.League.IntegrationTests
{
    public class WebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase("postgres")
            .WithUsername("user")
            .WithPassword("password")
            .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var descriptor = services
                    .SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<SpanishFootballLeagueDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<SpanishFootballLeagueDbContext>(options =>
                {
                    options
                        .UseNpgsql(_dbContainer.GetConnectionString())
                        .UseSnakeCaseNamingConvention();
                });
            });

            base.ConfigureWebHost(builder);
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
        }

        public async new Task DisposeAsync()
        {
            await _dbContainer.StopAsync();
        }
    }
}
