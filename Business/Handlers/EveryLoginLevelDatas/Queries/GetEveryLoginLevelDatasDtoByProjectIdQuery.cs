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

namespace Business.Handlers.EveryLoginLevelDatas.Queries
{
    public class GetEveryLoginLevelDatasDtoByProjectIdQuery : IRequest<IDataResult<IEnumerable<EveryLoginLevelDataDto>>>
    {
        public string ProjectID { get; set; }
        public class GetEveryLoginLevelDatasDtoByProjectIdQueryHandler : IRequestHandler<GetEveryLoginLevelDatasDtoByProjectIdQuery, IDataResult<IEnumerable<EveryLoginLevelDataDto>>>
        {
            private readonly IEveryLoginLevelDataRepository _everyLoginLevelDataRepository;
            private readonly IMediator _mediator;

            public GetEveryLoginLevelDatasDtoByProjectIdQueryHandler(IEveryLoginLevelDataRepository everyLoginLevelDataRepository, IMediator mediator)
            {
                _everyLoginLevelDataRepository = everyLoginLevelDataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ApacheKafkaDatabaseActionLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<EveryLoginLevelDataDto>>> Handle(GetEveryLoginLevelDatasDtoByProjectIdQuery request, CancellationToken cancellationToken)
            {
                var everyLoginLevelDataDtosList = new List<EveryLoginLevelDataDto>();

                var everyLoginLevelDatasList = await _everyLoginLevelDataRepository
                    .GetListAsync(p => p.ProjectID == request.ProjectID);

                everyLoginLevelDatasList.ToList().ForEach(item =>
                {
                    everyLoginLevelDataDtosList.Add(new EveryLoginLevelDataDto
                    {
                        AverageScores = item.AverageScores,
                        ClientId = item.ClientId,
                        Levelname = item.Levelname,
                        LevelsDifficultylevel = item.LevelsDifficultylevel,
                        PlayingTime = item.PlayingTime,
                        TotalPowerUsage = item.TotalPowerUsage
                    });
                });

                return new SuccessDataResult<IEnumerable<EveryLoginLevelDataDto>>(everyLoginLevelDataDtosList);
            }
        }
    }
}