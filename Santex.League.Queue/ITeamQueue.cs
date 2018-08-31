using Santex.League.Domain;
using System.Threading.Tasks;

namespace Santex.League.Queue
{
    public interface ITeamQueue
    {
        void SyncTeams(Team team);
    }
}
