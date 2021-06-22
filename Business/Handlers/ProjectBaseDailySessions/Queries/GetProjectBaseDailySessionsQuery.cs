
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

namespace Business.Handlers.ProjectBaseDailySessions.Queries
{

    public class GetProjectBaseDailySessionsQuery : IRequest<IDataResult<IEnumerable<ProjectDailySession>>>
    {
        public class GetProjectBaseDailySessionsQueryHandler : IRequestHandler<GetProjectBaseDailySessionsQuery, IDataResult<IEnumerable<ProjectDailySession>>>
        {
            private readonly IProjectBaseDailySessionRepository _projectBaseDailySessionRepository;
            private readonly IMediator _mediator;

            public GetProjectBaseDailySessionsQueryHandler(IProjectBaseDailySessionRepository projectBaseDailySessionRepository, IMediator mediator)
            {
                _projectBaseDailySessionRepository = projectBaseDailySessionRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ProjectDailySession>>> Handle(GetProjectBaseDailySessionsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ProjectDailySession>>(await _projectBaseDailySessionRepository.GetListAsync());
            }
        }
    }
}