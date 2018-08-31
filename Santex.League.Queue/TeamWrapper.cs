using AutoMapper;
using Hangfire;
using MediatR;
using Santex.League.Domain;
using Santex.League.Domain.Repository;
using Santex.League.Proxy.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Santex.League.Queue
{
    public class TeamWrapper
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITeamRepository _teamRepository;

        public TeamWrapper(IMediator mediator, IMapper mapper, IUnitOfWork unitOfWork, ITeamRepository teamRepository) {
            _mediator = mediator;
            _mapper = mapper;
            _teamRepository = teamRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(Team team)
        {
            var teamResponse = await _mediator.Send(new GetTeamRequest(team.Id));
            team.Players = _mapper.Map<IEnumerable<Player>>(teamResponse.Squad);
            team.SetTotalPlayers();

            await _teamRepository.Create(team);

            await _unitOfWork.SaveChangesAsync();

            
            //Free Tier (football-api)
            //12 competitions
            //Basic data(Fixtures, Results and League Tables)
            //10 API calls per minute
            await Task.Delay(1000 * 10);
        }
    }
}
