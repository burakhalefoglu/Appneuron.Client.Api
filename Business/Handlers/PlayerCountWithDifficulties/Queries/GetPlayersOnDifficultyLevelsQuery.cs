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

namespace Business.Handlers.PlayerCountWithDifficulties.Queries
{
    public class GetPlayersOnDifficultyLevelsQuery : IRequest<IDataResult<IEnumerable<PlayerCountWithDifficulty>>>
    {
        public class GetPlayersOnDifficultyLevelsQueryHandler : IRequestHandler<GetPlayersOnDifficultyLevelsQuery, IDataResult<IEnumerable<PlayerCountWithDifficulty>>>
        {
            private readonly IPlayerCountOnDifficultyLevelRepository _playersOnDifficultyLevelRepository;
            private readonly IMediator _mediator;

            public GetPlayersOnDifficultyLevelsQueryHandler(IPlayerCountOnDifficultyLevelRepository playersOnDifficultyLevelRepository, IMediator mediator)
            {
                _playersOnDifficultyLevelRepository = playersOnDifficultyLevelRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<PlayerCountWithDifficulty>>> Handle(GetPlayersOnDifficultyLevelsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<PlayerCountWithDifficulty>>(await _playersOnDifficultyLevelRepository.GetListAsync());
            }
        }
    }
}