
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

namespace Business.Handlers.ProjectBaseTotalDieWithDifficulties.Queries
{

    public class GetProjectBaseTotalDieWithDifficultiesQuery : IRequest<IDataResult<IEnumerable<ProjectBaseTotalDieWithDifficulty>>>
    {
        public class GetProjectBaseTotalDieWithDifficultiesQueryHandler : IRequestHandler<GetProjectBaseTotalDieWithDifficultiesQuery, IDataResult<IEnumerable<ProjectBaseTotalDieWithDifficulty>>>
        {
            private readonly IProjectBaseTotalDieWithDifficultyRepository _projectBaseTotalDieWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetProjectBaseTotalDieWithDifficultiesQueryHandler(IProjectBaseTotalDieWithDifficultyRepository projectBaseTotalDieWithDifficultyRepository, IMediator mediator)
            {
                _projectBaseTotalDieWithDifficultyRepository = projectBaseTotalDieWithDifficultyRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ProjectBaseTotalDieWithDifficulty>>> Handle(GetProjectBaseTotalDieWithDifficultiesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ProjectBaseTotalDieWithDifficulty>>(await _projectBaseTotalDieWithDifficultyRepository.GetListAsync());
            }
        }
    }
}