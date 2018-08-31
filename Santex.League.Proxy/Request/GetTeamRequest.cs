using MediatR;
using Santex.League.Proxy.Response;

namespace Santex.League.Proxy.Request
{
    public class GetTeamRequest : IRequest<GetTeamResponse>
    {
        public int TeamId { get; set; }

        public GetTeamRequest(int teamId) {
            TeamId = teamId;
        }
    }
}
