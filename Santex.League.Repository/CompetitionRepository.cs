using Microsoft.EntityFrameworkCore;
using Santex.League.Domain;
using Santex.League.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Santex.League.Repository
{
    public class CompetitionRepository : Repository<Competition>, ICompetitionRepository
    {
        public CompetitionRepository(LeagueDbContext context) : base(context) { }

    }
}
