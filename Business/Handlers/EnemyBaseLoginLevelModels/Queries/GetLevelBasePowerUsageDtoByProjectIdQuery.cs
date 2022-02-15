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
    public class GetLevelBasePowerUsageDtoByProjectIdQuery : IRequest<IDataResult<IEnumerable<LevelbasePowerUsageDto>>>
    {
        public long ProjectId { get; set; }

        public class GetLevelBasePowerUsageDtoByProjectIdQueryHandler : IRequestHandler<
            GetLevelBasePowerUsageDtoByProjectIdQuery, IDataResult<IEnumerable<LevelbasePowerUsageDto>>>
        {
            private readonly IEnemyBaseLoginLevelModelRepository _everyLoginLevelDataRepository;
            private readonly IMediator _mediator;

            public GetLevelBasePowerUsageDtoByProjectIdQueryHandler(
                IEnemyBaseLoginLevelModelRepository everyLoginLevelDataRepository, IMediator mediator)
            {
                _everyLoginLevelDataRepository = everyLoginLevelDataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<LevelbasePowerUsageDto>>> Handle(
                GetLevelBasePowerUsageDtoByProjectIdQuery request, CancellationToken cancellationToken)
            {
                var everyLoginLevelDataList = await _everyLoginLevelDataRepository.GetListAsync(
                    e => e.ProjectId == request.ProjectId && e.Status == true);
                var levelbasePowerUsageDtoList = new List<LevelbasePowerUsageDto>();


                everyLoginLevelDataList.ToList().ForEach(e =>
                {
                    var resultlevelbasePowerUsageDto =
                        levelbasePowerUsageDtoList.FirstOrDefault(t =>
                            t.ClientId == e.ClientId && t.Levelname == e.LevelName);
                    if (resultlevelbasePowerUsageDto != null)
                        resultlevelbasePowerUsageDto.TotalPowerUsage += e.TotalPowerUsage;
                    else
                        levelbasePowerUsageDtoList.Add(new LevelbasePowerUsageDto
                        {
                            ClientId = e.ClientId,
                            Levelname = e.LevelName,
                            TotalPowerUsage = e.TotalPowerUsage
                        });
                });


                return new SuccessDataResult<IEnumerable<LevelbasePowerUsageDto>>(levelbasePowerUsageDtoList);
            }
        }
    }
}