
using Business.BusinessAspects;
using Core.Utilities.Results;
using Core.Aspects.Autofac.Performance;
using DataAccess.Abstract;
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

    public class GetBuyingEventByProjectIdQuery : IRequest<IDataResult<IEnumerable<BuyingEventDto>>>
    {
        public long ProjectId { get; set; }
        public class GetBuyingEventByProjectIdQueryHandler : IRequestHandler<GetBuyingEventByProjectIdQuery, IDataResult<IEnumerable<BuyingEventDto>>>
        {
            private readonly IBuyingEventRepository _buyingEventRepository;

            public GetBuyingEventByProjectIdQueryHandler(IBuyingEventRepository buyingEventRepository)
            {
                _buyingEventRepository = buyingEventRepository;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<BuyingEventDto>>> Handle(GetBuyingEventByProjectIdQuery request, CancellationToken cancellationToken)
            {
                var buyingEventList = await _buyingEventRepository.GetListAsync(p => p.ProjectId == request.ProjectId && p.Status == true);
                var buyingEventDtoList = new List<BuyingEventDto>();

                buyingEventList.ToList().ForEach(b =>
                {

                    var buyingEventDto = buyingEventDtoList.FirstOrDefault(d => d.ClientId == b.ClientId
                    && d.TrigerdTime == new DateTime(b.TriggeredTime.Day,
                    b.TriggeredTime.Month,
                    b.TriggeredTime.Year));

                    if (buyingEventDto == null)
                    {
                        buyingEventDtoList.Add(new BuyingEventDto
                        {
                            ClientId = b.ClientId,
                            TrigerdTime = new DateTime(b.TriggeredTime.Day,
                                                b.TriggeredTime.Month,
                                                b.TriggeredTime.Year)
                        });
                    }



                });



                return new SuccessDataResult<IEnumerable<BuyingEventDto>>(buyingEventDtoList);
            }
        }
    }
}