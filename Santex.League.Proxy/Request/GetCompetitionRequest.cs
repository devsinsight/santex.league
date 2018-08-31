using MediatR;
using Santex.League.Proxy.Response;

namespace Santex.League.Proxy.Request
{
    public class GetCompetitionRequest : IRequest<GetCompetitionResponse>
    {
        public string Code { get; set; }

        public GetCompetitionRequest(string leagueCode)
        {
            Code = leagueCode;
        }
    }
}
