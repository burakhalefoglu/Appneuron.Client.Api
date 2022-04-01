using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.GameSessions.Queries;

public class GetDailySessionsQuery: IRequest<IDataResult<long[]>>
{
    public long ProjectId { get; set; }

    public class GetDailySessionsQueryHandler : IRequestHandler<GetDailySessionsQuery,
        IDataResult<long[]>>
    {
        private readonly IGameSessionRepository _gameSessionRepository;

        public GetDailySessionsQueryHandler(IGameSessionRepository gameSessionRepository)
        {
            _gameSessionRepository = gameSessionRepository;
        }

        [PerformanceAspect(5)]
        [CacheAspect(10)]
        [LogAspect(typeof(ConsoleLogger))]
        [SecuredOperation(Priority = 1)]
        public async Task<IDataResult<long[]>>Handle(GetDailySessionsQuery request,
            CancellationToken cancellationToken)
        {
            var clients = new List<long>();
            var client =
                await _gameSessionRepository.GetListAsync(c => c.ProjectId == request.ProjectId);
            for (var i = 0; i < 7; i++)
            {
                clients.Add(client.ToList().Where(x=> x.SessionStartTime < DateTimeOffset.Now.AddDays(-i) &&
                                                      x.SessionStartTime > DateTimeOffset.Now.AddDays(-i - 1)).ToList().Count);
            }
            return new SuccessDataResult<long[]>(clients.ToArray());
        }
    }
}