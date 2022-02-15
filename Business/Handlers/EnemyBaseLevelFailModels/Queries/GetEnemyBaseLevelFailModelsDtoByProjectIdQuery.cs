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

namespace Business.Handlers.EnemyBaseLevelFailModels.Queries
{
    public class GetEnemyBaseLevelFailModelsDtoByProjectIdQuery : IRequest<IDataResult<IEnumerable<LevelbaseFailDto>>>
    {
        public long ProjectId { get; set; }

        public class GetEnemyBaseLevelFailModelsDtoByProjectIdQueryHandler : IRequestHandler<GetEnemyBaseLevelFailModelsDtoByProjectIdQuery,
            IDataResult<IEnumerable<LevelbaseFailDto>>>
        {
            private readonly IEnemyBaseLevelFailModelRepository _levelBaseDieDataRepository;

            public GetEnemyBaseLevelFailModelsDtoByProjectIdQueryHandler(IEnemyBaseLevelFailModelRepository levelBaseDieDataRepository)
            {
                _levelBaseDieDataRepository = levelBaseDieDataRepository;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<LevelbaseFailDto>>> Handle(
                GetEnemyBaseLevelFailModelsDtoByProjectIdQuery request, CancellationToken cancellationToken)
            {
                var levelBaseDieDataList =
                    await _levelBaseDieDataRepository.GetListAsync(l =>
                        l.ProjectId == request.ProjectId && l.Status == true);
                var levelbaseFailDtoList = new List<LevelbaseFailDto>();

                levelBaseDieDataList.ToList().ForEach(levelBaseFail =>
                {
                    var levelbaseFailDto = levelbaseFailDtoList.FirstOrDefault(levelBaseFailDto =>
                        levelBaseFailDto.ClientId == levelBaseFail.ClientId
                        && levelBaseFailDto.LevelName == levelBaseFail.LevelName);

                    if (levelbaseFailDto == null)
                        levelbaseFailDtoList.Add(new LevelbaseFailDto
                        {
                            ClientId = levelBaseFail.ClientId,
                            FailCount = 1,
                            LevelName = levelBaseFail.LevelName
                        });
                    else
                        levelbaseFailDto.FailCount += 1;
                });

                return new SuccessDataResult<IEnumerable<LevelbaseFailDto>>(levelbaseFailDtoList);
            }
        }
    }
}