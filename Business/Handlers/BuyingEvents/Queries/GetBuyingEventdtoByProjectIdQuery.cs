
using Business.BusinessAspects;
using Core.Utilities.Results;
using Core.Aspects.Autofac.Performance;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Caching;
using Entities.Dtos;
using System.Linq;
using System;

namespace Business.Handlers.BuyingEvents.Queries
{

    public class GetBuyingEventdtoByProjectIdQuery : IRequest<IDataResult<IEnumerable<BuyingEventDto>>>
    {
        public string ProjectId { get; set; }
        public class GetBuyingEventdtoByProjectIdQueryHandler : IRequestHandler<GetBuyingEventdtoByProjectIdQuery, IDataResult<IEnumerable<BuyingEventDto>>>
        {
            private readonly IBuyingEventRepository _buyingEventRepository;
            private readonly IMediator _mediator;

            public GetBuyingEventdtoByProjectIdQueryHandler(IBuyingEventRepository buyingEventRepository, IMediator mediator)
            {
                _buyingEventRepository = buyingEventRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<BuyingEventDto>>> Handle(GetBuyingEventdtoByProjectIdQuery request, CancellationToken cancellationToken)
            {
                var buyingEventList = await _buyingEventRepository.GetListAsync(p => p.ProjectID == request.ProjectId);
                var buyingEventDtoList = new List<BuyingEventDto>();

                buyingEventList.ToList().ForEach(b =>
                {

                    var buyingEventDto = buyingEventDtoList.FirstOrDefault(bdto => bdto.ClientId == b.ClientId
                    && bdto.TrigerdTime == new DateTime(b.TrigerdTime.Day,
                    b.TrigerdTime.Month,
                    b.TrigerdTime.Year));

                    if (buyingEventDto == null)
                    {
                        buyingEventDtoList.Add(new BuyingEventDto
                        {
                            ClientId = b.ClientId,
                            TrigerdTime = new DateTime(b.TrigerdTime.Day,
                                                b.TrigerdTime.Month,
                                                b.TrigerdTime.Year)
                        });
                    }



                });



                return new SuccessDataResult<IEnumerable<BuyingEventDto>>(buyingEventDtoList);
            }
        }
    }
}