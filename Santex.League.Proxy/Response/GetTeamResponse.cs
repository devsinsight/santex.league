
using System;
using System.Collections.Generic;

namespace Santex.League.Proxy.Response
{
    public class GetTeamResponse
    {
        public int Id { get; set; }
        public Area Area { get; set; }
        public string ShortName { get; set; }
        public string Tla { get; set; }
        public string Email { get; set; }
        public IEnumerable<Player> Squad { get; set; }

        public string Message { get; set; }
        public bool IsSuccess => string.IsNullOrEmpty(Message);
    }

    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string CountryOfBirth { get; set; }
        public string Nationality { get; set; }
    }
}
