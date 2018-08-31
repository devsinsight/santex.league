using Hangfire;
using Santex.League.Domain;
using System;

namespace Santex.League.Queue
{
    public class TeamQueue : ITeamQueue
    {
        public void SyncTeams(Team team)
        {

            
            BackgroundJob.Enqueue<TeamWrapper>(p => p.Execute(team));
        }
    }
}
