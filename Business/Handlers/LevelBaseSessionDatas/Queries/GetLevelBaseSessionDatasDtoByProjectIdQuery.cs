using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.LevelBaseSessionDatas.Queries
{
    public class GetLevelBaseSessionDatasDtoByProjectIdQuery : IRequest<IDataResult<IEnumerable<LevelBaseSessionDataDto>>>
    {
        public string ProjectID { get; set; }

        public class GetLevelBaseSessionDatasDtoByProjectIdQueryHandler : IRequestHandler<GetLevelBaseSessionDatasDtoByProjectIdQuery, IDataResult<IEnumerable<LevelBaseSessionDataDto>>>
        {
            private readonly ILevelBaseSessionDataRepository _levelBaseSessionDataRepository;
            private readonly IMediator _mediator;

            public GetLevelBaseSessionDatasDtoByProjectIdQueryHandler(ILevelBaseSessionDataRepository levelBaseSessionDataRepository, IMediator mediator)
            {
                _levelBaseSessionDataRepository = levelBaseSessionDataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<LevelBaseSessionDataDto>>> Handle(GetLevelBaseSessionDatasDtoByProjectIdQuery request, CancellationToken cancellationToken)
            {
                var levelBaseSessionDataDtosList = new List<LevelBaseSessionDataDto>();
                var levelBaseSessionDatasList = await _levelBaseSessionDataRepository
                    .GetListAsync(p => p.ProjectID == request.ProjectID);
                levelBaseSessionDatasList.ToList().ForEach(item =>
                {
                    levelBaseSessionDataDtosList.Add(new LevelBaseSessionDataDto
                    {
                        ClientId = item.ClientId,
                        DifficultyLevel = item.DifficultyLevel,
                        levelName = item.levelName,
                        SessionFinishTime = item.SessionFinishTime,
                        SessionStartTime = item.SessionStartTime,
                        SessionTimeMinute = item.SessionTimeMinute
                    });
                });
                return new SuccessDataResult<IEnumerable<LevelBaseSessionDataDto>>(levelBaseSessionDataDtosList);
            }
        }
    }
}