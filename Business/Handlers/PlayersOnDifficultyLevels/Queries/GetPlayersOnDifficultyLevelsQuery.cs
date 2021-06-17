
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

namespace Business.Handlers.PlayersOnDifficultyLevels.Queries
{

    public class GetPlayersOnDifficultyLevelsQuery : IRequest<IDataResult<IEnumerable<ProjectBasePlayerCountOnDifficultyLevel>>>
    {
        public class GetPlayersOnDifficultyLevelsQueryHandler : IRequestHandler<GetPlayersOnDifficultyLevelsQuery, IDataResult<IEnumerable<ProjectBasePlayerCountOnDifficultyLevel>>>
        {
            private readonly IPlayersOnDifficultyLevelRepository _playersOnDifficultyLevelRepository;
            private readonly IMediator _mediator;

            public GetPlayersOnDifficultyLevelsQueryHandler(IPlayersOnDifficultyLevelRepository playersOnDifficultyLevelRepository, IMediator mediator)
            {
                _playersOnDifficultyLevelRepository = playersOnDifficultyLevelRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ProjectBasePlayerCountOnDifficultyLevel>>> Handle(GetPlayersOnDifficultyLevelsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ProjectBasePlayerCountOnDifficultyLevel>>(await _playersOnDifficultyLevelRepository.GetListAsync());
            }
        }
    }
}