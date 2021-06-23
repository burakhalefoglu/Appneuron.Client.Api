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

namespace Business.Handlers.DailySessionWithDifficulties.Queries
{
    public class GetDailySessionWithDifficultiesByProjectIdQuery : IRequest<IDataResult<IEnumerable<DailySessionWithDifficulty>>>
    {
        public string ProjectId { get; set; }

        public class GetDailySessionWithDifficultiesByProjectIdQueryHandler : IRequestHandler<GetDailySessionWithDifficultiesByProjectIdQuery, IDataResult<IEnumerable<DailySessionWithDifficulty>>>
        {
            private readonly IDailySessionWithDifficultyRepository _dailySessionWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetDailySessionWithDifficultiesByProjectIdQueryHandler(IDailySessionWithDifficultyRepository dailySessionWithDifficultyRepository, IMediator mediator)
            {
                _dailySessionWithDifficultyRepository = dailySessionWithDifficultyRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<DailySessionWithDifficulty>>> Handle(GetDailySessionWithDifficultiesByProjectIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<DailySessionWithDifficulty>>(await _dailySessionWithDifficultyRepository.GetListAsync(p => p.ProjectId == request.ProjectId));
            }
        }
    }
}