using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers.ApacheKafka;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.BuyingEvents.Queries
{
    public class GetBuyingEventsDtoByProjectIdQuery : IRequest<IDataResult<IEnumerable<BuyingEventDto>>>
    {
        public string ProjectID { get; set; }
        public class GetBuyingEventsDtoByProjectIdQueryHandler : IRequestHandler<GetBuyingEventsDtoByProjectIdQuery, IDataResult<IEnumerable<BuyingEventDto>>>
        {
            private readonly IBuyingEventRepository _buyingEventRepository;
            private readonly IMediator _mediator;

            public GetBuyingEventsDtoByProjectIdQueryHandler(IBuyingEventRepository buyingEventRepository, IMediator mediator)
            {
                _buyingEventRepository = buyingEventRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ApacheKafkaDatabaseActionLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<BuyingEventDto>>> Handle(GetBuyingEventsDtoByProjectIdQuery request, CancellationToken cancellationToken)
            {
                var buyingEventDtosList = new List<BuyingEventDto>();
                var buyingEventsList = await _buyingEventRepository.GetListAsync(p => p.ProjectID == request.ProjectID);

                buyingEventsList.ToList().ForEach(item =>
                {
                    buyingEventDtosList.Add(new BuyingEventDto
                    {
                        ClientId = item.ClientId,
                        DifficultyLevel = item.DifficultyLevel,
                        ProductType = item.ProductType,
                        TrigerdTime = item.TrigerdTime,
                        TrigersInlevelName = item.TrigersInlevelName
                    });

                });

                return new SuccessDataResult<IEnumerable<BuyingEventDto>>(buyingEventDtosList);
            }
        }
    }
}