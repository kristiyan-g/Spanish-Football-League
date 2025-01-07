using Microsoft.EntityFrameworkCore;
using Spanish.Football.League.Api.Extensions;
using Spanish.Football.League.Api.Middlewares;
using Spanish.Football.League.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
var services = builder.Services;

services.Register();

services.AddControllers();

services.AddHealthChecks();

services.AddEndpointsApiExplorer();
services.RegisterSwaggerGen();

services.AddDbContext<SpanishFootballLeagueDbContext>(options => options
    .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseSnakeCaseNamingConvention());

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapHealthChecks("api/healthcheck");

app.UseAuthorization();

app.MapControllers();

app.Run();

/// <summary>
/// Hacky approach to reference Program class in integration testing.
/// </summary>
public partial class Program { }