using AutoMapper;
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

namespace Business.Handlers.AdvEvents.Queries
{
    public class GetAdvEventsDtoByProjectIdQuery : IRequest<IDataResult<IEnumerable<AdvEventDto>>>
    {
        public string ProjectID { get; set; }

        public class GetAdvEventsDtoByProjectIdQueryHandler : IRequestHandler<GetAdvEventsDtoByProjectIdQuery, IDataResult<IEnumerable<AdvEventDto>>>
        {
            private readonly IAdvEventRepository _advEventRepository;
            private readonly IMediator _mediator;

            public GetAdvEventsDtoByProjectIdQueryHandler(IAdvEventRepository advEventRepository,
                IMediator mediator)
            {
                _advEventRepository = advEventRepository;
                _mediator = mediator;

            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ApacheKafkaDatabaseActionLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<AdvEventDto>>> Handle(GetAdvEventsDtoByProjectIdQuery request, CancellationToken cancellationToken)
            {
                var advEventDtosList = new List<AdvEventDto>();

                var advEventsList = await _advEventRepository.GetListAsync(p => p.ProjectID == request.ProjectID);
                advEventsList.ToList().ForEach(item =>
                {
                    advEventDtosList.Add(new AdvEventDto { 
                    
                        AdvType = item.AdvType,
                        ClientId = item.ClientId,
                        DifficultyLevel = item.DifficultyLevel,
                        TrigerdTime = item.TrigerdTime,
                        TrigersInlevelName = item.TrigersInlevelName
                    
                    });
                });
                return new SuccessDataResult<IEnumerable<AdvEventDto>>(advEventDtosList);
            }
        }
    }
}