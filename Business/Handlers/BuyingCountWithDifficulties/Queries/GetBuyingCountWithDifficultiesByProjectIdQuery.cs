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

namespace Business.Handlers.BuyingCountWithDifficulties.Queries
{
    public class GetBuyingCountWithDifficultiesByProjectIdQuery : IRequest<IDataResult<IEnumerable<BuyingCountWithDifficulty>>>
    {
        public string ProjectId { get; set; }

        public class GetBuyingCountWithDifficultiesByProjectIdQueryHandler : IRequestHandler<GetBuyingCountWithDifficultiesByProjectIdQuery, IDataResult<IEnumerable<BuyingCountWithDifficulty>>>
        {
            private readonly IBuyingCountWithDifficultyRepository _buyingCountWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetBuyingCountWithDifficultiesByProjectIdQueryHandler(IBuyingCountWithDifficultyRepository buyingCountWithDifficultyRepository, IMediator mediator)
            {
                _buyingCountWithDifficultyRepository = buyingCountWithDifficultyRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<BuyingCountWithDifficulty>>> Handle(GetBuyingCountWithDifficultiesByProjectIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<BuyingCountWithDifficulty>>(await _buyingCountWithDifficultyRepository.GetListAsync(p => p.ProjectId == request.ProjectId));
            }
        }
    }
}