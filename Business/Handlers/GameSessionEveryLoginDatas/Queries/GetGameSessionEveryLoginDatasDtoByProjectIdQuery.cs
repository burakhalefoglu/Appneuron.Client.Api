using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers.ApacheKafka;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.GameSessionEveryLoginDatas.Queries
{
    public class GetGameSessionEveryLoginDatasDtoByProjectIdQuery : IRequest<IDataResult<IEnumerable<GameSessionEveryLoginDataDto>>>
    {
        public string ProjectID { get; set; }

        public class GetGameSessionEveryLoginDatasDtoByProjectIdQueryHandler : IRequestHandler<GetGameSessionEveryLoginDatasDtoByProjectIdQuery, IDataResult<IEnumerable<GameSessionEveryLoginDataDto>>>
        {
            private readonly IGameSessionEveryLoginDataRepository _gameSessionEveryLoginDataRepository;
            private readonly IMediator _mediator;

            public GetGameSessionEveryLoginDatasDtoByProjectIdQueryHandler(IGameSessionEveryLoginDataRepository gameSessionEveryLoginDataRepository, IMediator mediator)
            {
                _gameSessionEveryLoginDataRepository = gameSessionEveryLoginDataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ApacheKafkaDatabaseActionLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<GameSessionEveryLoginDataDto>>> Handle(GetGameSessionEveryLoginDatasDtoByProjectIdQuery request, CancellationToken cancellationToken)
            {
                var gameSessionEveryLoginDataDtosList = new List<GameSessionEveryLoginDataDto>();

                var gameSessionEveryLoginDatasList = await _gameSessionEveryLoginDataRepository.GetListAsync(p => p.ProjectID == request.ProjectID);
                gameSessionEveryLoginDatasList.ToList().ForEach(item =>
                {
                    gameSessionEveryLoginDataDtosList.Add(new GameSessionEveryLoginDataDto
                    {
                        ClientId = item.ClientId,
                        SessionFinishTime = item.SessionFinishTime,
                        SessionStartTime = item.SessionStartTime,
                        SessionTimeMinute = item.SessionTimeMinute
                    });

                });
                return new SuccessDataResult<IEnumerable<GameSessionEveryLoginDataDto>>(gameSessionEveryLoginDataDtosList);
            }
        }
    }
}