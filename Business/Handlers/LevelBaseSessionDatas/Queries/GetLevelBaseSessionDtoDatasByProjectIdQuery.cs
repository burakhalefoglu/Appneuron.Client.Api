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

namespace Business.Handlers.LevelBaseSessionDatas.Queries
{
    public class GetLevelBaseSessionDtoDatasByProjectIdQuery : IRequest<IDataResult<IEnumerable<LevelbaseSessionDto>>>
    {
        public long ProjectId { get; set; }

        public class GetLevelBaseSessionDtoDatasByProjectIdQueryHandler : IRequestHandler<
            GetLevelBaseSessionDtoDatasByProjectIdQuery, IDataResult<IEnumerable<LevelbaseSessionDto>>>
        {
            private readonly ILevelBaseSessionDataRepository _levelBaseSessionDataRepository;
            private readonly IMediator _mediator;

            public GetLevelBaseSessionDtoDatasByProjectIdQueryHandler(
                ILevelBaseSessionDataRepository levelBaseSessionDataRepository, IMediator mediator)
            {
                _levelBaseSessionDataRepository = levelBaseSessionDataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<LevelbaseSessionDto>>> Handle(
                GetLevelBaseSessionDtoDatasByProjectIdQuery request, CancellationToken cancellationToken)
            {
                var sessionData =
                    await _levelBaseSessionDataRepository.GetListAsync(p =>
                        p.ProjectId == request.ProjectId && p.Status == true);
                var sessionDataDto = new List<LevelbaseSessionDto>();
                sessionData.ToList().ForEach(s =>
                {
                    sessionDataDto.Add(new LevelbaseSessionDto
                    {
                        ClientId = s.ClientId,
                        LevelName = s.LevelName,
                        SessionStartTime = s.SessionStartTime
                    });
                });
                return new SuccessDataResult<IEnumerable<LevelbaseSessionDto>>(sessionDataDto);
            }
        }
    }
}