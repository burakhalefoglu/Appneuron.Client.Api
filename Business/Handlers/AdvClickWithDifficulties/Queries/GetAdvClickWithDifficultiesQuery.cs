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

namespace Business.Handlers.AdvClickWithDifficulties.Queries
{
    public class GetAdvClickWithDifficultiesQuery : IRequest<IDataResult<IEnumerable<AdvClickWithDifficulty>>>
    {
        public class GetAdvClickWithDifficultiesQueryHandler : IRequestHandler<GetAdvClickWithDifficultiesQuery, IDataResult<IEnumerable<AdvClickWithDifficulty>>>
        {
            private readonly IAdvClickWithDifficultyRepository _advClickWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetAdvClickWithDifficultiesQueryHandler(IAdvClickWithDifficultyRepository advClickWithDifficultyRepository, IMediator mediator)
            {
                _advClickWithDifficultyRepository = advClickWithDifficultyRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<AdvClickWithDifficulty>>> Handle(GetAdvClickWithDifficultiesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<AdvClickWithDifficulty>>(await _advClickWithDifficultyRepository.GetListAsync());
            }
        }
    }
}