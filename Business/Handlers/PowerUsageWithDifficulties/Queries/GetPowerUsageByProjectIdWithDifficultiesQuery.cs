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

namespace Business.Handlers.PowerUsageWithDifficulties.Queries
{
    public class GetPowerUsageByProjectIdWithDifficultiesQuery : IRequest<IDataResult<IEnumerable<PowerUsageWithDifficulty>>>
    {
        public string ProjectId { get; set; }

        public class GetPowerUsageByProjectIdWithDifficultiesQueryHandler : IRequestHandler<GetPowerUsageByProjectIdWithDifficultiesQuery, IDataResult<IEnumerable<PowerUsageWithDifficulty>>>
        {
            private readonly IPowerUsageWithDifficultyRepository _powerUsageWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetPowerUsageByProjectIdWithDifficultiesQueryHandler(IPowerUsageWithDifficultyRepository powerUsageWithDifficultyRepository, IMediator mediator)
            {
                _powerUsageWithDifficultyRepository = powerUsageWithDifficultyRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<PowerUsageWithDifficulty>>> Handle(GetPowerUsageByProjectIdWithDifficultiesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<PowerUsageWithDifficulty>>(await _powerUsageWithDifficultyRepository.GetListAsync(p => p.ProjectId == request.ProjectId));
            }
        }
    }
}