
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

namespace Business.Handlers.ProjectBaseBuyingCountWithDifficulties.Queries
{

    public class GetProjectBaseBuyingCountWithDifficultiesQuery : IRequest<IDataResult<IEnumerable<ProjectBaseBuyingCountWithDifficulty>>>
    {
        public class GetProjectBaseBuyingCountWithDifficultiesQueryHandler : IRequestHandler<GetProjectBaseBuyingCountWithDifficultiesQuery, IDataResult<IEnumerable<ProjectBaseBuyingCountWithDifficulty>>>
        {
            private readonly IProjectBaseBuyingCountWithDifficultyRepository _projectBaseBuyingCountWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetProjectBaseBuyingCountWithDifficultiesQueryHandler(IProjectBaseBuyingCountWithDifficultyRepository projectBaseBuyingCountWithDifficultyRepository, IMediator mediator)
            {
                _projectBaseBuyingCountWithDifficultyRepository = projectBaseBuyingCountWithDifficultyRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ProjectBaseBuyingCountWithDifficulty>>> Handle(GetProjectBaseBuyingCountWithDifficultiesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ProjectBaseBuyingCountWithDifficulty>>(await _projectBaseBuyingCountWithDifficultyRepository.GetListAsync());
            }
        }
    }
}