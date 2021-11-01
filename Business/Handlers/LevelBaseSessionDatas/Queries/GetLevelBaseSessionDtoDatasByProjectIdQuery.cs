
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

namespace Business.Handlers.LevelBaseSessionDatas.Queries
{

    public class GetLevelBaseSessionDtoDatasByProjectIdQuery : IRequest<IDataResult<IEnumerable<LevelbaseSessionDto>>>
    {
        public string ProjectID { get; set; }

        public class GetLevelBaseSessionDtoDatasByProjectIdQueryHandler : IRequestHandler<GetLevelBaseSessionDtoDatasByProjectIdQuery, IDataResult<IEnumerable<LevelbaseSessionDto>>>
        {
            private readonly ILevelBaseSessionDataRepository _levelBaseSessionDataRepository;
            private readonly IMediator _mediator;

            public GetLevelBaseSessionDtoDatasByProjectIdQueryHandler(ILevelBaseSessionDataRepository levelBaseSessionDataRepository, IMediator mediator)
            {
                _levelBaseSessionDataRepository = levelBaseSessionDataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<LevelbaseSessionDto>>> Handle(GetLevelBaseSessionDtoDatasByProjectIdQuery request, CancellationToken cancellationToken)
            {
                var sessionData = await _levelBaseSessionDataRepository.GetListAsync(p => p.ProjectId == request.ProjectID);
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