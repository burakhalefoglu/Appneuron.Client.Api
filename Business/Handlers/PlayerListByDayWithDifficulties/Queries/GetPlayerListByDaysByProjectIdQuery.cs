using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.ChartModels;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PlayerListByDayWithDifficulties.Queries
{
    public class GetPlayerListByDaysByProjectIdQuery : IRequest<IDataResult<IEnumerable<PlayerListByDayWithDifficulty>>>
    {
        public string ProjectId { get; set; }

        public class GetPlayerListByDaysByProjectIdQueryHandler : IRequestHandler<GetPlayerListByDaysByProjectIdQuery, IDataResult<IEnumerable<PlayerListByDayWithDifficulty>>>
        {
            private readonly IPlayerListByDayWithDifficultyRepository _playerListByDayWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetPlayerListByDaysByProjectIdQueryHandler(IPlayerListByDayWithDifficultyRepository playerListByDayWithDifficultyRepository, IMediator mediator)
            {
                _playerListByDayWithDifficultyRepository = playerListByDayWithDifficultyRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<PlayerListByDayWithDifficulty>>> Handle(GetPlayerListByDaysByProjectIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<PlayerListByDayWithDifficulty>>(await _playerListByDayWithDifficultyRepository.GetListAsync(p => p.ProjectId == request.ProjectId));
            }
        }
    }
}