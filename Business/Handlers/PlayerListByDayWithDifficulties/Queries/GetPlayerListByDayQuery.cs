using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.ChartModels;
using MediatR;
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PlayerListByDaysWithDifficulty.Queries
{
    public class GetPlayerListByDayQuery : IRequest<IDataResult<PlayerListByDayWithDifficulty>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetPlayerListByDayQueryHandler : IRequestHandler<GetPlayerListByDayQuery, IDataResult<PlayerListByDayWithDifficulty>>
        {
            private readonly IPlayerListByDayWithDifficultyRepository _playerListByDayRepository;
            private readonly IMediator _mediator;

            public GetPlayerListByDayQueryHandler(IPlayerListByDayWithDifficultyRepository playerListByDayRepository, IMediator mediator)
            {
                _playerListByDayRepository = playerListByDayRepository;
                _mediator = mediator;
            }

            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<PlayerListByDayWithDifficulty>> Handle(GetPlayerListByDayQuery request, CancellationToken cancellationToken)
            {
                var playerListByDay = await _playerListByDayRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<PlayerListByDayWithDifficulty>(playerListByDay);
            }
        }
    }
}