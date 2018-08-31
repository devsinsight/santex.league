using Microsoft.Extensions.Configuration;

namespace Santex.League.Proxy.Utils
{
    public class FootballCredentials : IFootballCredentials
    {
        public FootballCredentials(IConfiguration configutation) {
            Token = configutation["FootballAPICredentials:Token"];
            BaseUrl = configutation["FootballAPICredentials:BaseUrl"];
        }

        public string Token { get; }

        public string BaseUrl { get; }
    }
}
