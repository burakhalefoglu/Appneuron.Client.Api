using System;
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

namespace Business.Handlers.LevelBaseSessionModels.Queries
{
    public class
        GetLevelbaseSessionWithPlayingTimeDtoByProjectIdQuery : IRequest<
            IDataResult<IEnumerable<LevelbaseSessionWithPlayingTimeDto>>>
    {
        public long ProjectId { get; set; }

        public class GetLevelbaseSessionWithPlayingTimeDtoByProjectIdQueryHandler : IRequestHandler<
            GetLevelbaseSessionWithPlayingTimeDtoByProjectIdQuery,
            IDataResult<IEnumerable<LevelbaseSessionWithPlayingTimeDto>>>
        {
            private readonly ILevelBaseSessionModelRepository _levelBaseSessionDataRepository;
            private readonly IMediator _mediator;

            public GetLevelbaseSessionWithPlayingTimeDtoByProjectIdQueryHandler(
                ILevelBaseSessionModelRepository levelBaseSessionDataRepository, IMediator mediator)
            {
                _levelBaseSessionDataRepository = levelBaseSessionDataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<LevelbaseSessionWithPlayingTimeDto>>> Handle(
                GetLevelbaseSessionWithPlayingTimeDtoByProjectIdQuery request, CancellationToken cancellationToken)
            {
                var sessionData =
                    await _levelBaseSessionDataRepository.GetListAsync(p =>
                        p.ProjectId == request.ProjectId && p.Status == true);
                var levelbaseSessionWithPlayingTimeDtoList = new List<LevelbaseSessionWithPlayingTimeDto>();
                sessionData.ToList().ForEach(s =>
                {
                    var resultLevelbaseSessionWithPlayingTimeDto = levelbaseSessionWithPlayingTimeDtoList
                        .Find(l => l.SessionStartTime == new DateTime(
                                       s.SessionStartTime.Day, s.SessionStartTime.Month, s.SessionStartTime.Year) &&
                                   l.LevelName == s.LevelName && l.ClientId == s.ClientId);

                    if (resultLevelbaseSessionWithPlayingTimeDto == null)
                        levelbaseSessionWithPlayingTimeDtoList.Add(
                            new LevelbaseSessionWithPlayingTimeDto
                            {
                                ClientId = s.ClientId,
                                LevelName = s.LevelName,
                                SessionStartTime = s.SessionStartTime,
                                SessionTimeMinute = s.SessionTimeMinute
                            });
                    else
                        resultLevelbaseSessionWithPlayingTimeDto.SessionTimeMinute += s.SessionTimeMinute;
                });

                return new SuccessDataResult<IEnumerable<LevelbaseSessionWithPlayingTimeDto>>(
                    levelbaseSessionWithPlayingTimeDtoList);
            }
        }
    }
}