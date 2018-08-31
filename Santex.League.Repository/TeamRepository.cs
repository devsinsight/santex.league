using Santex.League.Domain;
using Santex.League.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Santex.League.Repository
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        public TeamRepository(LeagueDbContext context) : base(context) { }
    
    
    }
}
