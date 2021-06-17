
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

namespace Business.Handlers.ProjectBasePowerUsageByDifficulties.Queries
{

    public class GetProjectBasePowerUsageByDifficultiesQuery : IRequest<IDataResult<IEnumerable<ProjectBasePowerUsageByDifficulty>>>
    {
        public class GetProjectBasePowerUsageByDifficultiesQueryHandler : IRequestHandler<GetProjectBasePowerUsageByDifficultiesQuery, IDataResult<IEnumerable<ProjectBasePowerUsageByDifficulty>>>
        {
            private readonly IProjectBasePowerUsageByDifficultyRepository _projectBasePowerUsageByDifficultyRepository;
            private readonly IMediator _mediator;

            public GetProjectBasePowerUsageByDifficultiesQueryHandler(IProjectBasePowerUsageByDifficultyRepository projectBasePowerUsageByDifficultyRepository, IMediator mediator)
            {
                _projectBasePowerUsageByDifficultyRepository = projectBasePowerUsageByDifficultyRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ProjectBasePowerUsageByDifficulty>>> Handle(GetProjectBasePowerUsageByDifficultiesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ProjectBasePowerUsageByDifficulty>>(await _projectBasePowerUsageByDifficultyRepository.GetListAsync());
            }
        }
    }
}