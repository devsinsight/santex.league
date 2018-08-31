using MediatR;
using Santex.League.Proxy.Request;
using Santex.League.Proxy.Response;
using Santex.League.Proxy.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Santex.League.Proxy.Handler
{
    public class TeamHandler :
        IRequestHandler<GetTeamRequest, GetTeamResponse>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IFootballCredentials _footballCredentials;

        public TeamHandler(IHttpClientFactory httpClientFactory, IFootballCredentials footballCredentials)
        {
            _httpClientFactory = httpClientFactory;
            _footballCredentials = footballCredentials;
        }

        public async Task<GetTeamResponse> Handle(GetTeamRequest request, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("X-Auth-Token", _footballCredentials.Token);
            client.Timeout = new TimeSpan(0, 0, 30);
            var response = await client.GetAsync(_footballCredentials.BaseUrl + "/v2/teams/" + request.TeamId);

            return await response.Content.ReadAsJsonAsync<GetTeamResponse>();
        }
    }

}
