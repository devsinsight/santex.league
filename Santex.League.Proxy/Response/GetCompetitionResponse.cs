using System;
using System.Collections.Generic;

namespace Santex.League.Proxy.Response
{
    public class GetCompetitionResponse
    {
        public Competition Competition { get; set; }
        public IEnumerable<Team> Teams { get; set; }

        public string Message { get; set; }
        public bool IsSuccess => string.IsNullOrEmpty(Message);
    }

    public class Competition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Area Area { get; set; }
        public string Code { get; set; }
        public string Plan { get; set; }
        public DateTime? LastUpdated { get; set; }

    }

    public class Team
    {
        public int Id { get; set; }
        public Area Area { get; set; }
        public string ShortName { get; set; }
        public string Tla { get; set; }
        public string Email { get; set; }
    }

    public class Area
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
