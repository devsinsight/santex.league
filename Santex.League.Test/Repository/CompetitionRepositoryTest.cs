using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Santex.League.Domain;
using Santex.League.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Santex.League.Test.Repository
{
    [TestClass]
    public class CompetitionRepositoryTest
    {
        [TestMethod]
        public void GetAllCompetitions()
        {
            var competitionRepo = new Mock<ICompetitionRepository>();
            competitionRepo.Setup(x => x.GetAll()).Returns(new List<Competition>
            {
                new Competition
                {
                    Id = 2002,
                    Name = "Bundesliga",
                    AreaName = "Germany",
                    Code = "BL1",
                    Plan = "TIER_ONE",
                    LastUpdated = new DateTime(2018,08,31)
                }
            });

            var result = competitionRepo.Object.GetAll();

            Assert.AreEqual(1, result.Count());
        }

    }
}
