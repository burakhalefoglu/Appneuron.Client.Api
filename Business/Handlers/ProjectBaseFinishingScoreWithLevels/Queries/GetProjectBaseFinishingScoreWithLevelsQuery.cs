
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

namespace Business.Handlers.ProjectBaseFinishingScoreWithLevels.Queries
{

    public class GetProjectBaseFinishingScoreWithLevelsQuery : IRequest<IDataResult<IEnumerable<FinishingScoreWithLevel>>>
    {
        public class GetProjectBaseFinishingScoreWithLevelsQueryHandler : IRequestHandler<GetProjectBaseFinishingScoreWithLevelsQuery, IDataResult<IEnumerable<FinishingScoreWithLevel>>>
        {
            private readonly IProjectBaseFinishingScoreWithLevelRepository _projectBaseFinishingScoreWithLevelRepository;
            private readonly IMediator _mediator;

            public GetProjectBaseFinishingScoreWithLevelsQueryHandler(IProjectBaseFinishingScoreWithLevelRepository projectBaseFinishingScoreWithLevelRepository, IMediator mediator)
            {
                _projectBaseFinishingScoreWithLevelRepository = projectBaseFinishingScoreWithLevelRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<FinishingScoreWithLevel>>> Handle(GetProjectBaseFinishingScoreWithLevelsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<FinishingScoreWithLevel>>(await _projectBaseFinishingScoreWithLevelRepository.GetListAsync());
            }
        }
    }
}