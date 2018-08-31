using Microsoft.AspNetCore.Mvc;
using Santex.League.API.Services;
using System.Net.Http;
using System.Threading.Tasks;

namespace Santex.League.API.Controllers
{
    [ApiController]
    public class LeagueController : ControllerBase
    {
        private readonly ILeagueService _competitionService;   

        public LeagueController(ILeagueService competitionService) {
            _competitionService = competitionService;
        }

        /// <summary>
        /// Import a League by code (leagueCode).
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /import-league/BL1
        ///
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>Store a League by code</returns>
        /// <response code="504">Server Error</response>
        /// <response code="409">League already imported</response>
        /// <response code="404">Not found</response>
        /// <response code="201">Successfully imported</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(504)]
        [HttpGet("/import-league/{leagueCode}")]
        public async Task<IActionResult> ImportLeague(string leagueCode)
        {
            var result = await _competitionService.Handle(leagueCode);

            return StatusCode(result.StatusCode, result.Content ?? result.ReasonPhrase);
        }

        /// <summary>
        /// Returns the total amount of players belonging to all teams that participate in the given league (leagueCode).
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /total-players/BL1
        ///
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>{ "total" : N }</returns>
        /// <response code="504">Server Error</response>
        /// <response code="404">Not found</response>
        /// <response code="200">Success</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(504)]
        [HttpGet("/total-players/{leagueCode}")]
        public IActionResult GetTotalPlayers(string leagueCode)
        {
            var result = _competitionService.GetTotalPlayers(leagueCode);

            return StatusCode(result.StatusCode, result.Content ?? result.ReasonPhrase);
        }

    }
}