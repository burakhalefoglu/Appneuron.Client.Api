using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos;
using MediatR;

namespace Business.Handlers.AdvEvents.Queries
{
    public class GetClickBaseAdvEventDtoListQuery : IRequest<IDataResult<IEnumerable<ClickbaseAdvEventDto>>>
    {
        public long ProjectId { get; set; }

        public class GetClickBaseAdvEventDtoListQueryHandler : IRequestHandler<GetClickBaseAdvEventDtoListQuery,
            IDataResult<IEnumerable<ClickbaseAdvEventDto>>>
        {
            private readonly IAdvEventRepository _advEventRepository;

            public GetClickBaseAdvEventDtoListQueryHandler(IAdvEventRepository advEventRepository)
            {
                _advEventRepository = advEventRepository;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)] 
            public async Task<IDataResult<IEnumerable<ClickbaseAdvEventDto>>> Handle(
                GetClickBaseAdvEventDtoListQuery request, CancellationToken cancellationToken)
            {
                var advEventList = await _advEventRepository.GetListAsync(adv
                    => adv.ProjectId == request.ProjectId && adv.Status == true);
                var clickBaseAdvEventDtoList = new List<ClickbaseAdvEventDto>();
                advEventList.ToList().ForEach(adv =>
                {
                    var filterDate = new DateTime(adv.TriggerTime.Day,
                        adv.TriggerTime.Month,
                        adv.TriggerTime.Year);
                    var resultEvent = clickBaseAdvEventDtoList.FirstOrDefault(c => c.TrigerdDay == filterDate);
                    if (resultEvent == null)
                    {
                        var clickBaseAdvEventDto = new ClickbaseAdvEventDto();
                        _ = clickBaseAdvEventDto.ClientClickDtoList.Append(new ClientClickDto
                        {
                            ClickCount = 1,
                            ClientId = adv.ClientId
                        });
                        clickBaseAdvEventDto.TrigerdDay = filterDate;
                        clickBaseAdvEventDtoList.Add(clickBaseAdvEventDto);
                    }
                    else
                    {
                        var clientResult =
                            resultEvent.ClientClickDtoList.FirstOrDefault(c => c.ClientId == adv.ClientId);
                        if (clientResult == null)
                            _ = resultEvent.ClientClickDtoList.Append(new ClientClickDto
                            {
                                ClientId = adv.ClientId,
                                ClickCount = 1
                            });
                        else
                            clientResult.ClickCount += 1;
                    }
                });


                return new SuccessDataResult<IEnumerable<ClickbaseAdvEventDto>>(clickBaseAdvEventDtoList);
            }
        }
    }
}