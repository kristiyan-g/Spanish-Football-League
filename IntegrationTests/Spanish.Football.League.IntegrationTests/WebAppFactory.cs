namespace Spanish.Football.League.IntegrationTests
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Spanish.Football.League.Database;
    using Testcontainers.PostgreSql;
    using Testcontainers.Redis;

    public class WebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase("postgres")
            .WithUsername("user")
            .WithPassword("password")
            .Build();

        private readonly RedisContainer redisContainer = new RedisBuilder()
            .WithImage("redis:latest")
            .WithCleanUp(true)
            .WithPortBinding(0)
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
                        .UseNpgsql(dbContainer.GetConnectionString())
                        .UseSnakeCaseNamingConvention();
                });

                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = redisContainer.GetConnectionString();
                });
            });

            base.ConfigureWebHost(builder);
        }

        public async Task InitializeAsync()
        {
            await dbContainer.StartAsync();
            await redisContainer.StartAsync();
        }

        public async new Task DisposeAsync()
        {
            await dbContainer.StopAsync();
            await redisContainer.StopAsync();
        }
    }
}
