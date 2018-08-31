using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Santex.League.Domain
{
    public class Team
    {
        public Team()
        {
            Players = new List<Player>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int CompetitionId { get; set; }
        public string AreaName { get; set; }
        public string ShortName { get; set; }
        public string Tla { get; set; }
        public string Email { get; set; }

        public virtual IEnumerable<Player> Players { get; set; }
        public virtual Competition Competition { get; set; }

        public int TotalPlayers { get; set; }

        public void SetTotalPlayers() => TotalPlayers = Players?.Count() ?? 0;
        
    }
}
