
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

namespace Business.Handlers.ProjectBaseAdvClicks.Queries
{

    public class GetProjectBaseAdvClicksQuery : IRequest<IDataResult<IEnumerable<ProjectBaseAdvClick>>>
    {
        public class GetProjectBaseAdvClicksQueryHandler : IRequestHandler<GetProjectBaseAdvClicksQuery, IDataResult<IEnumerable<ProjectBaseAdvClick>>>
        {
            private readonly IProjectBaseAdvClickRepository _projectBaseAdvClickRepository;
            private readonly IMediator _mediator;

            public GetProjectBaseAdvClicksQueryHandler(IProjectBaseAdvClickRepository projectBaseAdvClickRepository, IMediator mediator)
            {
                _projectBaseAdvClickRepository = projectBaseAdvClickRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ProjectBaseAdvClick>>> Handle(GetProjectBaseAdvClicksQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ProjectBaseAdvClick>>(await _projectBaseAdvClickRepository.GetListAsync());
            }
        }
    }
}