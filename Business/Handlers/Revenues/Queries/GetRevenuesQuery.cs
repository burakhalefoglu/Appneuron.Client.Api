
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

namespace Business.Handlers.Revenues.Queries
{

    public class GetRevenuesQuery : IRequest<IDataResult<IEnumerable<ProjectBaseRevenue>>>
    {
        public class GetRevenuesQueryHandler : IRequestHandler<GetRevenuesQuery, IDataResult<IEnumerable<ProjectBaseRevenue>>>
        {
            private readonly IRevenueRepository _revenueRepository;
            private readonly IMediator _mediator;

            public GetRevenuesQueryHandler(IRevenueRepository revenueRepository, IMediator mediator)
            {
                _revenueRepository = revenueRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ProjectBaseRevenue>>> Handle(GetRevenuesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ProjectBaseRevenue>>(await _revenueRepository.GetListAsync());
            }
        }
    }
}