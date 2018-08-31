using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Santex.League.API.Services
{
    public interface ILeagueService
    {
        CustomHttpResponseMessage GetTotalPlayers(string leagueCode);
        Task<CustomHttpResponseMessage> Handle(string leagueCode);
    }
}
