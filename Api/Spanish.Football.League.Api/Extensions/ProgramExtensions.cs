﻿namespace Spanish.Football.League.Api.Extensions
{
    using System.Reflection;
    using FluentValidation;
    using Microsoft.OpenApi.Models;
    using Spanish.Football.League.Common.Models;
    using Spanish.Football.League.Common.Validations;
    using Spanish.Football.League.Repository;
    using Spanish.Football.League.Services;
    using Spanish.Football.League.Services.Interfaces;
    using Spanish.Football.League.Services.Mappers;

    /// <summary>
    /// A static class that contains extension methods for configuring and registering services
    /// in the application's dependency injection container. These methods are intended to
    /// be called during application startup to register essential services, such as
    /// core application components, singleton services, and other dependencies needed
    /// throughout the application.
    /// </summary>
    public static class ProgramExtensions
    {
        /// <summary>
        /// Registers core services to the specified IServiceCollection, enabling dependency injection
        /// for essential services used throughout the application.
        /// </summary>
        /// <param name="services">The IServiceCollection instance where services will be registered.</param>
        /// <returns>An IServiceCollection instance.</returns>
        public static IServiceCollection Register(this IServiceCollection services)
        {
            RegisterAllBaseServices(services);

            // Scoped
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped<MapperlyProfile>();
            services.AddScoped<IFootballLeagueService, FootballLeagueService>();
            services.AddScoped<IGameEngineService, GameEngineService>();

            return services;
        }

        /// <summary>
        /// Registers IValidator fluent validations to the specified IServiceCollection, enabling dependency injection
        /// for essential services used throughout the application.
        /// </summary>
        /// <param name="services">The IServiceCollection instance where services will be registered.</param>
        /// <returns>An IServiceCollection instance.</returns>
        public static IServiceCollection RegisterValidations(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateSeasonRequestDto>, CreateSeasonRequestDtoValidator>();

            return services;
        }

        /// <summary>
        /// Configures Redis Cache for the application.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The application configuration.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection RegisterRedisCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                options.InstanceName = "SpanishFootballLeague_";
            });

            return services;
        }

        private static void RegisterAllBaseServices(this IServiceCollection services)
        {
            // Get the assembly containing the services
            var assembly = Assembly.GetExecutingAssembly();

            // Find all types implementing IBaseService
            var baseServiceTypes = assembly.GetTypes()
                .Where(t => typeof(IBaseService).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

            // Register each found service type with the IServiceCollection
            foreach (var type in baseServiceTypes)
            {
                services.AddScoped(typeof(IBaseService), type);
            }
        }

        /// <summary>
        /// Configure all swagger generation settings.
        /// </summary>
        /// <param name="services">The IServiceCollection instance where services will be registered.</param>
        public static void RegisterSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Spanish Football League", Version = "v1" });
            });
        }
    }
}
