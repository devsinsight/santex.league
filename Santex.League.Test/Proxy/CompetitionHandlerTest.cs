using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Santex.League.Proxy.Handler;
using Santex.League.Proxy.Request;
using Santex.League.Proxy.Response;
using Santex.League.Proxy.Utils;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Santex.League.Test.Proxy
{
    [TestClass]
    public class CompetitionHandlerTest
    {
        [TestMethod]
        public async Task GetCompetitionResponseTest()
        {
            var competitionRequest = new Mock<GetCompetitionRequest>("LEAGUE_CODE");

            var competitionResponse = Task.FromResult(new GetCompetitionResponse
            {
                Competition = new Competition
                {
                    Id = 2002,
                    Name = "Bundesliga",
                    Area = new Area {
                        Id = 1,
                        Name = "Germany"
                    },
                    Code = "BL1",
                    Plan = "TIER_ONE",
                    LastUpdated = new DateTime(2018, 08, 31)
                }
            });

            
            var competitionHandler = new Mock<IRequestHandler<GetCompetitionRequest, GetCompetitionResponse>>();
            competitionHandler.Setup(x => x.Handle(competitionRequest.Object, new CancellationToken()))
                              .Returns(competitionResponse);

            var result = await competitionHandler.Object.Handle(competitionRequest.Object, new CancellationToken());

            Assert.AreEqual("Bundesliga", result.Competition.Name);
        }
    }
}
