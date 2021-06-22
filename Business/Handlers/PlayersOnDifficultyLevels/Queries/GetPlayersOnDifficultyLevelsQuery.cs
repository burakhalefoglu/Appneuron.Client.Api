
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;
using Entities.Concrete.ChartModels;

namespace Business.Handlers.PlayerCountOnDifficultyLevels.Queries
{

    public class GetPlayersOnDifficultyLevelsQuery : IRequest<IDataResult<IEnumerable<PlayerCountOnDifficultyLevel>>>
    {
        public class GetPlayersOnDifficultyLevelsQueryHandler : IRequestHandler<GetPlayersOnDifficultyLevelsQuery, IDataResult<IEnumerable<PlayerCountOnDifficultyLevel>>>
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
            public async Task<IDataResult<IEnumerable<PlayerCountOnDifficultyLevel>>> Handle(GetPlayersOnDifficultyLevelsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<PlayerCountOnDifficultyLevel>>(await _playersOnDifficultyLevelRepository.GetListAsync());
            }
        }
    }
}