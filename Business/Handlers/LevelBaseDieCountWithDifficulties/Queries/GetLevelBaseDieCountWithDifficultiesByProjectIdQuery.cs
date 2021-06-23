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

namespace Business.Handlers.LevelBaseDieCountWithDifficulties.Queries
{
    public class GetLevelBaseDieCountWithDifficultiesByProjectIdQuery : IRequest<IDataResult<IEnumerable<LevelBaseDieCountWithDifficulty>>>
    {
        public string ProjectId { get; set; }

        public class GetLevelBaseDieCountWithDifficultiesByProjectIdQueryHandler : IRequestHandler<GetLevelBaseDieCountWithDifficultiesByProjectIdQuery, IDataResult<IEnumerable<LevelBaseDieCountWithDifficulty>>>
        {
            private readonly ILevelBaseDieCountWithDifficultyRepository _levelBaseDieCountWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetLevelBaseDieCountWithDifficultiesByProjectIdQueryHandler(ILevelBaseDieCountWithDifficultyRepository levelBaseDieCountWithDifficultyRepository, IMediator mediator)
            {
                _levelBaseDieCountWithDifficultyRepository = levelBaseDieCountWithDifficultyRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<LevelBaseDieCountWithDifficulty>>> Handle(GetLevelBaseDieCountWithDifficultiesByProjectIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<LevelBaseDieCountWithDifficulty>>(await _levelBaseDieCountWithDifficultyRepository.GetListAsync(p => p.ProjectId == request.ProjectId));
            }
        }
    }
}