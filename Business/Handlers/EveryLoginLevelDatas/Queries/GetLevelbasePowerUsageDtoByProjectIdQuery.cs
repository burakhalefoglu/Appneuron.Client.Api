
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

namespace Business.Handlers.EveryLoginLevelDatas.Queries
{

    public class GetLevelbasePowerUsageDtoByProjectIdQuery : IRequest<IDataResult<IEnumerable<LevelbasePowerUsageDto>>>
    {
        public string ProjectId { get; set; }
        public class GetLevelbasePowerUsageDtoByProjectIdQueryHandler : IRequestHandler<GetLevelbasePowerUsageDtoByProjectIdQuery, IDataResult<IEnumerable<LevelbasePowerUsageDto>>>
        {
            private readonly IEveryLoginLevelDataRepository _everyLoginLevelDataRepository;
            private readonly IMediator _mediator;

            public GetLevelbasePowerUsageDtoByProjectIdQueryHandler(IEveryLoginLevelDataRepository everyLoginLevelDataRepository, IMediator mediator)
            {
                _everyLoginLevelDataRepository = everyLoginLevelDataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<LevelbasePowerUsageDto>>> Handle(GetLevelbasePowerUsageDtoByProjectIdQuery request, CancellationToken cancellationToken)
            {
                var everyLoginLevelDataList = await _everyLoginLevelDataRepository.GetListAsync(e => e.ProjectID == request.ProjectId);
                var levelbasePowerUsageDtoList = new List<LevelbasePowerUsageDto>();

                everyLoginLevelDataList.ToList().ForEach(e =>
                {

                    var resultlevelbasePowerUsageDto = levelbasePowerUsageDtoList.FirstOrDefault(t => t.ClientId == e.ClientId && t.Levelname == e.Levelname);
                    if(resultlevelbasePowerUsageDto != null)
                    {
                        resultlevelbasePowerUsageDto.TotalPowerUsage += e.TotalPowerUsage;
                    }
                    else
                    {
                        levelbasePowerUsageDtoList.Add(new LevelbasePowerUsageDto
                        {
                            ClientId = e.ClientId,
                            Levelname = e.Levelname,
                            TotalPowerUsage = e.TotalPowerUsage
                        });
                    }
                });


                return new SuccessDataResult<IEnumerable<LevelbasePowerUsageDto>>(levelbasePowerUsageDtoList);
            }
        }
    }
}