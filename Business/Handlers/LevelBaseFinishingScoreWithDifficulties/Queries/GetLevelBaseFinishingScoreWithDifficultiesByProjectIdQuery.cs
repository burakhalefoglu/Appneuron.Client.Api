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
    public class GetLevelBaseFinishingScoreWithDifficultiesByProjectIdQuery : IRequest<IDataResult<IEnumerable<LevelBaseFinishingScoreWithDifficulty>>>
    {
        public string ProjectId { get; set; }

        public class GetLevelBaseFinishingScoreWithDifficultiesByProjectIdQueryHandler : IRequestHandler<GetLevelBaseFinishingScoreWithDifficultiesByProjectIdQuery, IDataResult<IEnumerable<LevelBaseFinishingScoreWithDifficulty>>>
        {
            private readonly ILevelBaseFinishingScoreWithDifficultyRepository _levelBaseFinishingScoreWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetLevelBaseFinishingScoreWithDifficultiesByProjectIdQueryHandler(ILevelBaseFinishingScoreWithDifficultyRepository levelBaseFinishingScoreWithDifficultyRepository, IMediator mediator)
            {
                _levelBaseFinishingScoreWithDifficultyRepository = levelBaseFinishingScoreWithDifficultyRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<LevelBaseFinishingScoreWithDifficulty>>> Handle(GetLevelBaseFinishingScoreWithDifficultiesByProjectIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<LevelBaseFinishingScoreWithDifficulty>>(await _levelBaseFinishingScoreWithDifficultyRepository.GetListAsync(p => p.ProjectId == request.ProjectId));
            }
        }
    }
}