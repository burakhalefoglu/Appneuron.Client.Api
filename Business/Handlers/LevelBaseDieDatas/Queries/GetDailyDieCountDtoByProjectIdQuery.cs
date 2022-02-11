
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

namespace Business.Handlers.LevelBaseDieDatas.Queries
{

    public class GetDailyDieCountDtoByProjectIdQuery : IRequest<IDataResult<IEnumerable<DailyDieCountDto>>>
    {
        public long ProjectId { get; set; }
        public class GetDailyDieCountDtoQueryHandler : IRequestHandler<GetDailyDieCountDtoByProjectIdQuery, IDataResult<IEnumerable<DailyDieCountDto>>>
        {
            private readonly ILevelBaseDieDataRepository _levelBaseDieDataRepository;
            private readonly IMediator _mediator;

            public GetDailyDieCountDtoQueryHandler(ILevelBaseDieDataRepository levelBaseDieDataRepository, IMediator mediator)
            {
                _levelBaseDieDataRepository = levelBaseDieDataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<DailyDieCountDto>>> Handle(GetDailyDieCountDtoByProjectIdQuery request, CancellationToken cancellationToken)
            {
                var levelBaseDieDataList = await _levelBaseDieDataRepository.GetListAsync(l => l.ProjectId == request.ProjectId && l.Status == true);
                var dailyDieCountDtoList = new List<DailyDieCountDto>();

                levelBaseDieDataList.ToList().ForEach(l =>
                {
                    var dailyDieCountDto = dailyDieCountDtoList.FirstOrDefault(d => d.ClientId == l.ClientId && d.DateTime == l.DateTime);
                    if(dailyDieCountDto!= null)
                    {
                        dailyDieCountDto.DieCount += 1;
                    }
                    else
                    {
                        dailyDieCountDtoList.Add(new DailyDieCountDto
                        {
                            ClientId = l.ClientId,
                            DateTime = l.DateTime,
                            DieCount = 1
                        });
                    }

                });



                return new SuccessDataResult<IEnumerable<DailyDieCountDto>>(dailyDieCountDtoList);
            }
        }
    }
}