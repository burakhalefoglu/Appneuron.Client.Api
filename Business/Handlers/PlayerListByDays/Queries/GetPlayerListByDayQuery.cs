
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;

namespace Business.Handlers.PlayerListByDays.Queries
{
    public class GetPlayerListByDayQuery : IRequest<IDataResult<ProjectBasePlayerListByDayWithDifficulty>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetPlayerListByDayQueryHandler : IRequestHandler<GetPlayerListByDayQuery, IDataResult<ProjectBasePlayerListByDayWithDifficulty>>
        {
            private readonly IPlayerListByDayRepository _playerListByDayRepository;
            private readonly IMediator _mediator;

            public GetPlayerListByDayQueryHandler(IPlayerListByDayRepository playerListByDayRepository, IMediator mediator)
            {
                _playerListByDayRepository = playerListByDayRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ProjectBasePlayerListByDayWithDifficulty>> Handle(GetPlayerListByDayQuery request, CancellationToken cancellationToken)
            {
                var playerListByDay = await _playerListByDayRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<ProjectBasePlayerListByDayWithDifficulty>(playerListByDay);
            }
        }
    }
}
