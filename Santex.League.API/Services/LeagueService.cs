using AutoMapper;
using MediatR;
using Santex.League.Domain;
using Santex.League.Domain.Repository;
using Santex.League.Proxy.Request;
using Santex.League.Queue;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Santex.League.API.Services
{
    public class LeagueService : ILeagueService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompetitionRepository _competitionRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamQueue _teamQueue;

        public LeagueService(
            IMediator mediator, 
            IMapper mapper, 
            IUnitOfWork unitOfWork, 
            ICompetitionRepository competitionRepository,
            ITeamRepository teamRepository,
            ITeamQueue teamQueue)
        {
            _mediator = mediator;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _competitionRepository = competitionRepository;
            _teamRepository = teamRepository;
            _teamQueue = teamQueue;
        }

        public CustomHttpResponseMessage GetTotalPlayers(string leagueCode)
        {
            var competition = _competitionRepository.GetSingle(x => x.Code == leagueCode.ToUpper(), i => i.Teams);
            var totalPlayers = competition?.Teams.Sum(x => x.TotalPlayers);

            if (!totalPlayers.HasValue)
                return CustomHttpResponseMessage.NotFound();

            return CustomHttpResponseMessage.TotalPlayers(totalPlayers.Value);
        }

        public async Task<CustomHttpResponseMessage> Handle(string leagueCode)
        {
            if (_competitionRepository.GetList(x => x.Code == leagueCode.ToUpper()).Any())
                return CustomHttpResponseMessage.LeagueAlreadyImported();

            var competitionResponse = await _mediator.Send(new GetCompetitionRequest(leagueCode));

            if (!competitionResponse.IsSuccess)
                return CustomHttpResponseMessage.ServerError();

            if (competitionResponse.Competition == null)
                return CustomHttpResponseMessage.NotFound();

            SaveTeam(await SaveCompetition(competitionResponse));

            return CustomHttpResponseMessage.LeagueCreated();
        }

        private async Task<Competition> SaveCompetition(Proxy.Response.GetCompetitionResponse competitionResponse) {
            var competition = _mapper.Map<Competition>(competitionResponse.Competition);

            await _competitionRepository.Create(competition);

            await _unitOfWork.SaveChangesAsync();

            competition.Teams = _mapper.Map<IEnumerable<Team>>(competitionResponse.Teams);

            return competition;
        }

        private void SaveTeam(Competition competition) {
            foreach (var team in competition.Teams)
            {
                team.CompetitionId = competition.Id;
                _teamQueue.SyncTeams(team);
            }
        }
    }
}
