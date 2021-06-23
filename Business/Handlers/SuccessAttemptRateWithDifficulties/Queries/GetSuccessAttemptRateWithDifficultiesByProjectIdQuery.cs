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

namespace Business.Handlers.SuccessAttemptRateWithDifficulties.Queries
{
    public class GetSuccessAttemptRateWithDifficultiesByProjectIdQuery : IRequest<IDataResult<IEnumerable<SuccessAttemptRateWithDifficulty>>>
    {
        public string ProjectId { get; set; }

        public class GetSuccessAttemptRateWithDifficultiesByProjectIdQueryHandler : IRequestHandler<GetSuccessAttemptRateWithDifficultiesByProjectIdQuery, IDataResult<IEnumerable<SuccessAttemptRateWithDifficulty>>>
        {
            private readonly ISuccessAttemptRateWithDifficultyRepository _successAttemptRateWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetSuccessAttemptRateWithDifficultiesByProjectIdQueryHandler(ISuccessAttemptRateWithDifficultyRepository successAttemptRateWithDifficultyRepository, IMediator mediator)
            {
                _successAttemptRateWithDifficultyRepository = successAttemptRateWithDifficultyRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<SuccessAttemptRateWithDifficulty>>> Handle(GetSuccessAttemptRateWithDifficultiesByProjectIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<SuccessAttemptRateWithDifficulty>>(await _successAttemptRateWithDifficultyRepository.GetListAsync());
            }
        }
    }
}