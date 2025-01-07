namespace Spanish.Football.League.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Spanish.Football.League.DomainModels;
    using Spanish.Football.League.Repository;

    [Route("api/[controller]")]
    [ApiController]
    public class TeamController(
        IGenericRepository<Team, int> repository)
        : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetTeams()
        {
            // Retrieve all teams from the database
            var teams = repository.GetAll();

            // If there are no teams, return a 404 Not Found
            if (!teams.Any())
            {
                return NotFound();
            }

            // Return the list of teams with a 200 OK status
            return Ok(teams);
        }
    }
}
