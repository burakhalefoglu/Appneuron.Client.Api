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

namespace Business.Handlers.LevelBasePlayerCountWithDifficulties.Queries
{
    public class GetLevelBasePlayerCountWithDifficultiesByProjectIdQuery : IRequest<IDataResult<IEnumerable<LevelBasePlayerCountWithDifficulty>>>
    {
        public string ProjectId { get; set; }

        public class GetLevelBasePlayerCountWithDifficultiesByProjectIdQueryHandler : IRequestHandler<GetLevelBasePlayerCountWithDifficultiesByProjectIdQuery, IDataResult<IEnumerable<LevelBasePlayerCountWithDifficulty>>>
        {
            private readonly ILevelBasePlayerCountWithDifficultyRepository _levelBasePlayerCountWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetLevelBasePlayerCountWithDifficultiesByProjectIdQueryHandler(ILevelBasePlayerCountWithDifficultyRepository levelBasePlayerCountWithDifficultyRepository, IMediator mediator)
            {
                _levelBasePlayerCountWithDifficultyRepository = levelBasePlayerCountWithDifficultyRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<LevelBasePlayerCountWithDifficulty>>> Handle(GetLevelBasePlayerCountWithDifficultiesByProjectIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<LevelBasePlayerCountWithDifficulty>>(await _levelBasePlayerCountWithDifficultyRepository.GetListAsync(p => p.ProjectId == request.ProjectId));
            }
        }
    }
}