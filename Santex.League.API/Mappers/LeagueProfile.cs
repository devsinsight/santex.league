using AutoMapper;
using System.Linq;

namespace Santex.League.API.Mappers
{
    public class LeagueProfile : Profile
    {
        public LeagueProfile()
        {
            CreateMap<Proxy.Response.Competition, Domain.Competition>()
                .ForMember(m => m.AreaName, opt => opt.MapFrom(src => src.Area.Name));
            CreateMap<Proxy.Response.Team, Domain.Team>()
                .ForMember(m => m.AreaName, opt => opt.MapFrom(src => src.Area.Name));

            CreateMap<Proxy.Response.Player, Domain.Player>();
                
     
        }
    }
}
