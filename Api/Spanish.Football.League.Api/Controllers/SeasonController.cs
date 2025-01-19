namespace Spanish.Football.League.Api.Controllers
{
    using FluentValidation;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Newtonsoft.Json;
    using Spanish.Football.League.Api.Constants;
    using Spanish.Football.League.Common.Models;
    using Spanish.Football.League.Services.Interfaces;

    /// <summary>
    /// Season API Controller.
    /// </summary>
    [Route(EndpointConstants.SeasonBaseApi)]
    [ApiController]
    public class SeasonController(
        ILogger<SeasonController> logger,
        IDistributedCache cache,
        IFootballLeagueService footballLeagueService,
        IValidator<CreateSeasonRequestDto> validator)
        : ControllerBase
    {
        /// <summary>
        /// Endpoint for creating football season.
        /// </summary>
        /// <param name="request">Object containing information for the season.</param>
        /// <returns>Season ID.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSeason([FromBody] CreateSeasonRequestDto request)
        {
            var validationResults = await validator.ValidateAsync(request);
            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults);
            }

            var seasonId = await footballLeagueService.CreateSeasonAsync(request);

            if (seasonId == null)
            {
                var message = "Cannot create more than one season per year!";
                logger.LogWarning(message);

                return BadRequest(message);
            }

            logger.LogDebug($"New season with season ID {seasonId} successfully created!");

            return Created(string.Empty, seasonId);
        }

        /// <summary>
        /// Endpoint for retrieving the results of a specific season.
        /// </summary>
        /// <param name="seasonId">The ID of the season for which results are requested.</param>
        /// <returns>A <see cref="Task{TResult}">representing the result of the asynchronous operation.</returns>.
        [HttpGet("results/{seasonId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSeasonResultsAsync([FromRoute] int seasonId)
        {
            if (seasonId < 1)
            {
                var message = "Season ID must be greater that 0!";
                logger.LogWarning(message);

                return BadRequest(message);
            }

            string cacheKey = $"SeasonResults_{seasonId}";
            var cachedResult = await cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedResult))
            {
                logger.LogDebug($"Returning cached result for Season ID {seasonId}.");
                return Ok(JsonConvert.DeserializeObject<SeasonResultsResponseDto>(cachedResult));
            }

            var resultResponse = await footballLeagueService.GetSeasonResultsAsync(seasonId);
            if (resultResponse == null)
            {
                var message = $"No results found for season ID {seasonId}!";
                logger.LogDebug(message);

                return NotFound(message);
            }

            var jsonResponse = JsonConvert.SerializeObject(resultResponse);
            var cacheOptions = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(10));

            await cache.SetStringAsync(cacheKey, jsonResponse, cacheOptions);

            return Ok(resultResponse);
        }

        /// <summary>
        /// Endpoint for retrieving the leaderboard (season stats) of a specific season.
        /// </summary>
        /// <param name="seasonId">The ID of the season for which the leaderboard is requested.</param>
        /// <returns>A <see cref="Task{TResult}">representing the result of the asynchronous operation.</returns>.
        [HttpGet("{seasonId}/leaderboard")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSeasonStatsAsync([FromRoute] int seasonId)
        {
            if (seasonId < 1)
            {
                var message = "Season ID must be greater that 0!";
                logger.LogWarning(message);

                return BadRequest(message);
            }

            string cacheKey = $"SeasonStats_{seasonId}";
            var cachedStats = await cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedStats))
            {
                logger.LogDebug($"Returning cached stats for Season ID {seasonId}.");
                return Ok(JsonConvert.DeserializeObject<IEnumerable<SeasonStatsResponseDto>>(cachedStats));
            }

            var seasonStatsResponse = await footballLeagueService.GetSeasonStatsAsync(seasonId);
            if (seasonStatsResponse == null)
            {
                var message = $"No leaderboard found for season ID {seasonId}!";
                logger.LogDebug(message);

                return NotFound(message);
            }

            var jsonStats = JsonConvert.SerializeObject(seasonStatsResponse);
            var cacheOptions = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(10));

            await cache.SetStringAsync(cacheKey, jsonStats, cacheOptions);

            return Ok(seasonStatsResponse);
        }

        /// <summary>
        /// Endpoint for retrieving the team details for a specific season.
        /// </summary>
        /// <param name="seasonId">The ID of the season for which team details are requested.</param>
        /// <returns>A <see cref="Task{TResult}">representing the result of the asynchronous operation.</returns>.
        [HttpGet("{seasonId}/teamdetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTeamDetailsAsync([FromRoute] int seasonId)
        {
            if (seasonId < 1)
            {
                var message = "Season ID must be greater than 0!";
                logger.LogWarning(message);

                return BadRequest(message);
            }

            string cacheKey = $"TeamDetails_{seasonId}";
            var cachedTeamDetails = await cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedTeamDetails))
            {
                logger.LogDebug($"Returning cached team details for Season ID {seasonId}.");
                return Ok(JsonConvert.DeserializeObject<IEnumerable<TeamDetailsResponseDto>>(cachedTeamDetails));
            }

            var teamDetailsResponse = await footballLeagueService.GetTeamDetailsAsync(seasonId);
            if (teamDetailsResponse == null)
            {
                var message = $"No team details found for season ID {seasonId}!";
                logger.LogDebug(message);

                return NotFound(message);
            }

            var jsonTeamDetails = JsonConvert.SerializeObject(teamDetailsResponse);
            var cacheOptions = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(10));

            await cache.SetStringAsync(cacheKey, jsonTeamDetails, cacheOptions);

            return Ok(teamDetailsResponse);
        }
    }
}
