namespace Spanish.Football.League.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Spanish.Football.League.DomainModels;
    using Spanish.Football.League.Repository;
    using Spanish.Football.League.Services.Mappers;

    /// <summary>
    /// Tests api controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TestController(
        IGenericRepository<Team, int> repository,
        MapperlyProfile mapperly)
        : ControllerBase
    {
        /// <summary>
        /// GET endpoint at "api/Test/teams" to retrieve teams information.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation. Returns all teams if found, otherwise a NotFound status.</returns>
        [HttpGet("teams")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTeams()
        {
            var teams = repository.GetAll();

            if (!teams.Any())
            {
                return NotFound(new { Message = "There are no saved Teams." });
            }

            var teamDto = mapperly.MapToResponseDtoList(teams);

            return Ok(teamDto);
        }
    }
}
