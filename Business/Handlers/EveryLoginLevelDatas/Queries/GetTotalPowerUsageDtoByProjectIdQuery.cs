
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

namespace Business.Handlers.EveryLoginLevelDatas.Queries
{

    public class GetTotalPowerUsageDtoByProjectIdQuery : IRequest<IDataResult<IEnumerable<TotalPowerUsageDto>>>
    {
        public string ProjectId { get; set; }
        public class GetTotalPowerUsageDtoByProjectIdHandler : IRequestHandler<GetTotalPowerUsageDtoByProjectIdQuery, IDataResult<IEnumerable<TotalPowerUsageDto>>>
        {
            private readonly IEveryLoginLevelDataRepository _everyLoginLevelDataRepository;
            private readonly IMediator _mediator;

            public GetTotalPowerUsageDtoByProjectIdHandler(IEveryLoginLevelDataRepository everyLoginLevelDataRepository, IMediator mediator)
            {
                _everyLoginLevelDataRepository = everyLoginLevelDataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<TotalPowerUsageDto>>> Handle(GetTotalPowerUsageDtoByProjectIdQuery request, CancellationToken cancellationToken)
            {

                var everyLoginLevelDataList = await _everyLoginLevelDataRepository.GetListAsync(e => e.ProjectID == request.ProjectId);
                var totalPowerUsageDtoList = new List<TotalPowerUsageDto>();

                everyLoginLevelDataList.ToList().ForEach(e =>
                {
                    var resultPowerUsageDto = totalPowerUsageDtoList.FirstOrDefault(t => t.ClientId == e.ClientId && t.Date == e.DateTime);
                   
                    if(resultPowerUsageDto!= null)
                    {
                        resultPowerUsageDto.TotalPowerUsage += e.TotalPowerUsage;
                    }
                    else
                    {
                        totalPowerUsageDtoList.Add(new TotalPowerUsageDto
                        {
                            ClientId = e.ClientId,
                            TotalPowerUsage = e.TotalPowerUsage,
                            Date = e.DateTime
                        });
                    }
                });



                return new SuccessDataResult<IEnumerable<TotalPowerUsageDto>>(totalPowerUsageDtoList);
            }
        }
    }
}