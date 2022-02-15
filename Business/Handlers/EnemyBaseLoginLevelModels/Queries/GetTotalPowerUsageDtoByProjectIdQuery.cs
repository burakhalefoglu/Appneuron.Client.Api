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

namespace Business.Handlers.EnemyBaseLoginLevelModels.Queries
{
    public class GetTotalPowerUsageDtoByProjectIdQuery : IRequest<IDataResult<IEnumerable<TotalPowerUsageDto>>>
    {
        public long ProjectId { get; set; }

        public class GetTotalPowerUsageDtoByProjectIdHandler : IRequestHandler<GetTotalPowerUsageDtoByProjectIdQuery,
            IDataResult<IEnumerable<TotalPowerUsageDto>>>
        {
            private readonly IEnemyBaseLoginLevelModelRepository _everyLoginLevelDataRepository;
            private readonly IMediator _mediator;

            public GetTotalPowerUsageDtoByProjectIdHandler(IEnemyBaseLoginLevelModelRepository everyLoginLevelDataRepository,
                IMediator mediator)
            {
                _everyLoginLevelDataRepository = everyLoginLevelDataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<TotalPowerUsageDto>>> Handle(
                GetTotalPowerUsageDtoByProjectIdQuery request, CancellationToken cancellationToken)
            {
                var everyLoginLevelDataList =
                    await _everyLoginLevelDataRepository.GetListAsync(e => e.ProjectId == request.ProjectId);
                var totalPowerUsageDtoList = new List<TotalPowerUsageDto>();

                everyLoginLevelDataList.ToList().ForEach(e =>
                {
                    var resultPowerUsageDto = totalPowerUsageDtoList.FirstOrDefault(
                        t => t.ClientId == e.ClientId && t.Date == e.DateTime && e.Status == true);

                    if (resultPowerUsageDto != null)
                        resultPowerUsageDto.TotalPowerUsage += e.TotalPowerUsage;
                    else
                        totalPowerUsageDtoList.Add(new TotalPowerUsageDto
                        {
                            ClientId = e.ClientId,
                            TotalPowerUsage = e.TotalPowerUsage,
                            Date = e.DateTime
                        });
                });


                return new SuccessDataResult<IEnumerable<TotalPowerUsageDto>>(totalPowerUsageDtoList);
            }
        }
    }
}