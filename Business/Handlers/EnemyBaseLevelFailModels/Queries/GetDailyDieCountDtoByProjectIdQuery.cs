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

namespace Business.Handlers.LevelBaseDieDatas.Queries
{
    public class GetDailyDieCountDtoByProjectIdQuery : IRequest<IDataResult<IEnumerable<DailyDieCountDto>>>
    {
        public long ProjectId { get; set; }

        public class GetDailyDieCountDtoQueryHandler : IRequestHandler<GetDailyDieCountDtoByProjectIdQuery,
            IDataResult<IEnumerable<DailyDieCountDto>>>
        {
            private readonly IEnemyBaseLevelFailModelRepository _levelBaseDieDataRepository;
            private readonly IMediator _mediator;

            public GetDailyDieCountDtoQueryHandler(IEnemyBaseLevelFailModelRepository levelBaseDieDataRepository,
                IMediator mediator)
            {
                _levelBaseDieDataRepository = levelBaseDieDataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<DailyDieCountDto>>> Handle(
                GetDailyDieCountDtoByProjectIdQuery request, CancellationToken cancellationToken)
            {
                var levelBaseDieDataList =
                    await _levelBaseDieDataRepository.GetListAsync(l =>
                        l.ProjectId == request.ProjectId && l.Status == true);
                var dailyDieCountDtoList = new List<DailyDieCountDto>();

                levelBaseDieDataList.ToList().ForEach(l =>
                {
                    var dailyDieCountDto =
                        dailyDieCountDtoList.FirstOrDefault(d => d.ClientId == l.ClientId && d.DateTime == l.DateTime);
                    if (dailyDieCountDto != null)
                        dailyDieCountDto.DieCount += 1;
                    else
                        dailyDieCountDtoList.Add(new DailyDieCountDto
                        {
                            ClientId = l.ClientId,
                            DateTime = l.DateTime,
                            DieCount = 1
                        });
                });


                return new SuccessDataResult<IEnumerable<DailyDieCountDto>>(dailyDieCountDtoList);
            }
        }
    }
}