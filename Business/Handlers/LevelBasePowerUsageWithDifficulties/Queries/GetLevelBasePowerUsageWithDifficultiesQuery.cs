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
    public class GetLevelBasePowerUsageWithDifficultiesQuery : IRequest<IDataResult<IEnumerable<LevelBasePowerUsageWithDifficulty>>>
    {
        public class GetLevelBasePowerUsageWithDifficultiesQueryHandler : IRequestHandler<GetLevelBasePowerUsageWithDifficultiesQuery, IDataResult<IEnumerable<LevelBasePowerUsageWithDifficulty>>>
        {
            private readonly ILevelBasePowerUsageWithDifficultyRepository _levelBasePowerUsageWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetLevelBasePowerUsageWithDifficultiesQueryHandler(ILevelBasePowerUsageWithDifficultyRepository levelBasePowerUsageWithDifficultyRepository, IMediator mediator)
            {
                _levelBasePowerUsageWithDifficultyRepository = levelBasePowerUsageWithDifficultyRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<LevelBasePowerUsageWithDifficulty>>> Handle(GetLevelBasePowerUsageWithDifficultiesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<LevelBasePowerUsageWithDifficulty>>(await _levelBasePowerUsageWithDifficultyRepository.GetListAsync());
            }
        }
    }
}