using System.Net.Http;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Santex.League.Proxy.Request;
using Santex.League.Proxy.Response;
using Santex.League.Proxy.Utils;
using System.Linq;
using System;

namespace Santex.League.Proxy.Handler
{
    public class CompetitionHandler :
        IRequestHandler<GetCompetitionRequest, GetCompetitionResponse>
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IFootballCredentials _footballCredentials;

        public CompetitionHandler(IHttpClientFactory httpClientFactory, IFootballCredentials footballCredentials) {
            _httpClientFactory = httpClientFactory;
            _footballCredentials = footballCredentials;
        }

        public async Task<GetCompetitionResponse> Handle(GetCompetitionRequest request, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("X-Auth-Token", _footballCredentials.Token);
            var response = await client.GetAsync(_footballCredentials.BaseUrl + "/v2/competitions/ "+ request.Code.ToUpper() + "/teams");

            return await response.Content.ReadAsJsonAsync<GetCompetitionResponse>();
        }
    }
}
