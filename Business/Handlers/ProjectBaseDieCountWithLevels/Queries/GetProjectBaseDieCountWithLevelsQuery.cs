
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

namespace Business.Handlers.ProjectBaseDieCountWithLevels.Queries
{

    public class GetProjectBaseDieCountWithLevelsQuery : IRequest<IDataResult<IEnumerable<DieCountWithLevel>>>
    {
        public class GetProjectBaseDieCountWithLevelsQueryHandler : IRequestHandler<GetProjectBaseDieCountWithLevelsQuery, IDataResult<IEnumerable<DieCountWithLevel>>>
        {
            private readonly IProjectBaseDieCountWithLevelRepository _projectBaseDieCountWithLevelRepository;
            private readonly IMediator _mediator;

            public GetProjectBaseDieCountWithLevelsQueryHandler(IProjectBaseDieCountWithLevelRepository projectBaseDieCountWithLevelRepository, IMediator mediator)
            {
                _projectBaseDieCountWithLevelRepository = projectBaseDieCountWithLevelRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<DieCountWithLevel>>> Handle(GetProjectBaseDieCountWithLevelsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<DieCountWithLevel>>(await _projectBaseDieCountWithLevelRepository.GetListAsync());
            }
        }
    }
}