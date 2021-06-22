
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

namespace Business.Handlers.ProjectBaseDetaildSessions.Queries
{

    public class GetProjectBaseDetaildSessionsQuery : IRequest<IDataResult<IEnumerable<DetaildSession>>>
    {
        public class GetProjectBaseDetaildSessionsQueryHandler : IRequestHandler<GetProjectBaseDetaildSessionsQuery, IDataResult<IEnumerable<DetaildSession>>>
        {
            private readonly IProjectBaseDetaildSessionRepository _projectBaseDetaildSessionRepository;
            private readonly IMediator _mediator;

            public GetProjectBaseDetaildSessionsQueryHandler(IProjectBaseDetaildSessionRepository projectBaseDetaildSessionRepository, IMediator mediator)
            {
                _projectBaseDetaildSessionRepository = projectBaseDetaildSessionRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<DetaildSession>>> Handle(GetProjectBaseDetaildSessionsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<DetaildSession>>(await _projectBaseDetaildSessionRepository.GetListAsync());
            }
        }
    }
}