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
    public class GetLevelBaseFinishScoreByProjectIdQuery : IRequest<IDataResult<IEnumerable<LevelbaseFinishScoreDto>>>
    {
        public long ProjectId { get; set; }

        public class GetLevelBaseFinishScoreByProjectIdQueryHandler : IRequestHandler<
            GetLevelBaseFinishScoreByProjectIdQuery, IDataResult<IEnumerable<LevelbaseFinishScoreDto>>>
        {
            private readonly IEnemyBaseLoginLevelModelRepository _everyLoginLevelDataRepository;
            private readonly IMediator _mediator;

            public GetLevelBaseFinishScoreByProjectIdQueryHandler(
                IEnemyBaseLoginLevelModelRepository everyLoginLevelDataRepository, IMediator mediator)
            {
                _everyLoginLevelDataRepository = everyLoginLevelDataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<LevelbaseFinishScoreDto>>> Handle(
                GetLevelBaseFinishScoreByProjectIdQuery request, CancellationToken cancellationToken)
            {
                var everyLoginLevelDataList =
                    await _everyLoginLevelDataRepository.GetListAsync(e => e.ProjectId == request.ProjectId);
                var levelbaseFinishScoreDtoList = new List<LevelbaseFinishScoreDto>();

                everyLoginLevelDataList.ToList().ForEach(e =>
                {
                    var resultlevelbasePowerUsageDto = levelbaseFinishScoreDtoList.FirstOrDefault(t =>
                        t.ClientId == e.ClientId && t.Levelname == e.LevelName && e.Status == true);
                    if (resultlevelbasePowerUsageDto != null)
                        resultlevelbasePowerUsageDto.FinishScores += e.AverageScores;
                    else
                        levelbaseFinishScoreDtoList.Add(new LevelbaseFinishScoreDto
                        {
                            ClientId = e.ClientId,
                            Levelname = e.LevelName,
                            FinishScores = e.AverageScores
                        });
                });

                return new SuccessDataResult<IEnumerable<LevelbaseFinishScoreDto>>(levelbaseFinishScoreDtoList);
            }
        }
    }
}