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

namespace Business.Handlers.LevelBasePowerUsageWithDifficulties.Queries
{
    public class GetLevelBasePowerUsageWithDifficultiesByProjectIdQuery : IRequest<IDataResult<IEnumerable<LevelBasePowerUsageWithDifficulty>>>
    {
        public string ProjectId { get; set; }

        public class GetLevelBasePowerUsageWithDifficultiesByProjectIdQueryHandler : IRequestHandler<GetLevelBasePowerUsageWithDifficultiesByProjectIdQuery, IDataResult<IEnumerable<LevelBasePowerUsageWithDifficulty>>>
        {
            private readonly ILevelBasePowerUsageWithDifficultyRepository _levelBasePowerUsageWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetLevelBasePowerUsageWithDifficultiesByProjectIdQueryHandler(ILevelBasePowerUsageWithDifficultyRepository levelBasePowerUsageWithDifficultyRepository, IMediator mediator)
            {
                _levelBasePowerUsageWithDifficultyRepository = levelBasePowerUsageWithDifficultyRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<LevelBasePowerUsageWithDifficulty>>> Handle(GetLevelBasePowerUsageWithDifficultiesByProjectIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<LevelBasePowerUsageWithDifficulty>>(await _levelBasePowerUsageWithDifficultyRepository.GetListAsync(p => p.ProjectId == request.ProjectId));
            }
        }
    }
}