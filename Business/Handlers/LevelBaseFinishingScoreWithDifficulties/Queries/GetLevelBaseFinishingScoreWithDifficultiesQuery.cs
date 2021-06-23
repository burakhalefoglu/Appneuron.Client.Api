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

namespace Business.Handlers.LevelBaseFinishingScoreWithDifficulties.Queries
{
    public class GetLevelBaseFinishingScoreWithDifficultiesQuery : IRequest<IDataResult<IEnumerable<LevelBaseFinishingScoreWithDifficulty>>>
    {
        public class GetLevelBaseFinishingScoreWithDifficultiesQueryHandler : IRequestHandler<GetLevelBaseFinishingScoreWithDifficultiesQuery, IDataResult<IEnumerable<LevelBaseFinishingScoreWithDifficulty>>>
        {
            private readonly ILevelBaseFinishingScoreWithDifficultyRepository _levelBaseFinishingScoreWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetLevelBaseFinishingScoreWithDifficultiesQueryHandler(ILevelBaseFinishingScoreWithDifficultyRepository levelBaseFinishingScoreWithDifficultyRepository, IMediator mediator)
            {
                _levelBaseFinishingScoreWithDifficultyRepository = levelBaseFinishingScoreWithDifficultyRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<LevelBaseFinishingScoreWithDifficulty>>> Handle(GetLevelBaseFinishingScoreWithDifficultiesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<LevelBaseFinishingScoreWithDifficulty>>(await _levelBaseFinishingScoreWithDifficultyRepository.GetListAsync());
            }
        }
    }
}