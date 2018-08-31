using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Santex.League.Domain
{
    public class Competition
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }

        public string AreaName { get; set; }

        public string Code { get; set; }
        public string Plan { get; set; }
        public DateTime? LastUpdated { get; set; }

        
        public IEnumerable<Team> Teams { get; set; }
        
    }
}
