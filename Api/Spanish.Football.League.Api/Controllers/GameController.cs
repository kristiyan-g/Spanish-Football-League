namespace Spanish.Football.League.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Spanish.Football.League.Common.Models;
    using Spanish.Football.League.Services;

    [Route("api/[controller]")]
    [ApiController]
    public class GameController(
        GameEngineService gameEngineService)
        : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> StartSeason([FromBody] GameSetupDto gameSetup)
        {
            var teams = gameEngineService.GenerateTeams(gameSetup);
            //return Ok(teams);
            var teamss = teams.ToList();

            var matches = gameEngineService.GenerateMatches(teamss);
            return Ok(matches);
        }
    }
}
