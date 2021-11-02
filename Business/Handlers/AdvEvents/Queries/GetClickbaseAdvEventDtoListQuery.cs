
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

namespace Business.Handlers.AdvEvents.Queries
{

    public class GetClickBaseAdvEventDtoListQuery : IRequest<IDataResult<IEnumerable<ClickbaseAdvEventDto>>>
    {
        public string ProjectId { get; set; }
        public class GetClickBaseAdvEventDtoListQueryHandler : IRequestHandler<GetClickBaseAdvEventDtoListQuery, IDataResult<IEnumerable<ClickbaseAdvEventDto>>>
        {
            private readonly IAdvEventRepository _advEventRepository;
            private readonly IMediator _mediator;

            public GetClickBaseAdvEventDtoListQueryHandler(IAdvEventRepository advEventRepository, IMediator mediator)
            {
                _advEventRepository = advEventRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ClickbaseAdvEventDto>>> Handle(GetClickBaseAdvEventDtoListQuery request, CancellationToken cancellationToken)
            {
                var advEventList = await _advEventRepository.GetListAsync(adv => adv.ProjectId == request.ProjectId);
                var clickbaseAdvEventDtoList = new List<ClickbaseAdvEventDto>();
                advEventList.ToList().ForEach(adv =>
                {
                    var filterDate = new DateTime(adv.TrigerdTime.Day,
                        adv.TrigerdTime.Month,
                        adv.TrigerdTime.Year);
                    var resultEvent = clickbaseAdvEventDtoList.FirstOrDefault(c => c.TrigerdDay == filterDate);
                    if (resultEvent == null)
                    {
                        var clickbaseAdvEventDto = new ClickbaseAdvEventDto();
                        clickbaseAdvEventDto.ClientClickDtoList.Append(new ClientClickDto
                        {
                            ClickCount = 1,
                            ClientId = adv.ClientId
                        });
                        clickbaseAdvEventDto.TrigerdDay = filterDate;
                        clickbaseAdvEventDtoList.Add(clickbaseAdvEventDto);
                    }
                    else
                    {
                        var clientResult = resultEvent.ClientClickDtoList.FirstOrDefault(c => c.ClientId == adv.ClientId);
                        if(clientResult == null)
                        {
                            resultEvent.ClientClickDtoList.Append(new ClientClickDto
                            {
                                ClientId = adv.ClientId,
                                ClickCount = 1
                            });
                        }
                        else
                        {
                            clientResult.ClickCount += 1;
                        }
                    }
                });


                return new SuccessDataResult<IEnumerable<ClickbaseAdvEventDto>>(clickbaseAdvEventDtoList);
            }
        }
    }
}