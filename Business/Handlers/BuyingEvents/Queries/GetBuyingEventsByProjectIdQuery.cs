using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.BuyingEvents.Queries
{
    public class GetBuyingEventsByProjectIdQuery : IRequest<IDataResult<IEnumerable<BuyingEvent>>>
    {
        public long ProjectId { get; set; }

        public class GetBuyingEventsByProjectIdQueryHandler : IRequestHandler<GetBuyingEventsByProjectIdQuery,
            IDataResult<IEnumerable<BuyingEvent>>>
        {
            private readonly IBuyingEventRepository _buyingEventRepository;

            public GetBuyingEventsByProjectIdQueryHandler(IBuyingEventRepository buyingEventRepository)
            {
                _buyingEventRepository = buyingEventRepository;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<BuyingEvent>>> Handle(GetBuyingEventsByProjectIdQuery request,
                CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<BuyingEvent>>
                (await _buyingEventRepository.GetListAsync(
                    p => p.ProjectId == request.ProjectId && p.Status == true));
            }
        }
    }
}