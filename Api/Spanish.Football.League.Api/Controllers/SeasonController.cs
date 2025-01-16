namespace Spanish.Football.League.Api.Controllers
{
    using FluentValidation;
    using Microsoft.AspNetCore.Mvc;
    using Spanish.Football.League.Common.Models;
    using Spanish.Football.League.DomainModels;
    using Spanish.Football.League.Repository;
    using Spanish.Football.League.Services.Interfaces;

    /// <summary>
    /// Season API Controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonController(
        IFootballLeagueService footballLeagueService,
        IGenericRepository<Season, int> seasonRepository,
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

            var season = seasonRepository.GetAll().FirstOrDefault(x => x.SeasonYear == request.SeasonYear);

            if (season != null)
            {
                return BadRequest(new { Message = "Cannot create more than one season per year!" });
            }

            var seasonId = await footballLeagueService.CreateSeasonAsync(request);
            return Ok(seasonId);
        }

        //[HttpGet("results/{seasonId}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> GetSeasonResultsAsync([FromRoute] int seasonId)
        //{

        //}
    }
}
