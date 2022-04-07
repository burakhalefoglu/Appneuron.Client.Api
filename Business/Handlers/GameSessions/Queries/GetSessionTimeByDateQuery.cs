using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.GameSessions.Queries;

public class GetSessionTimeByDateQuery : IRequest<IDataResult<long>>
{
    public long ProjectId { get; set; }

    public DateTime Date { get; set; }

    public class GetSessionTimeByDateQueryHandler : IRequestHandler<GetSessionTimeByDateQuery,
        IDataResult<long>>
    {
        private readonly IGameSessionRepository _gameSessionRepository;

        public GetSessionTimeByDateQueryHandler(IGameSessionRepository gameSessionRepository)
        {
            _gameSessionRepository = gameSessionRepository;
        }

        [PerformanceAspect(5)]
        [CacheAspect(10)]
        [LogAspect(typeof(ConsoleLogger))]
        [SecuredOperation(Priority = 1)]
        public async Task<IDataResult<long>> Handle(GetSessionTimeByDateQuery request,
            CancellationToken cancellationToken)
        {
            long totalSessionTime = 0;
            var session =
                await _gameSessionRepository.GetListAsync(c => c.ProjectId == request.ProjectId);
            var resultSession = session.ToList().Where(x => x.SessionStartTime <= DateTimeOffset.Now.AddSeconds(20) &&
                                                            x.SessionStartTime >= DateTimeOffset.Now.AddSeconds(-20))
                .ToList();
            resultSession.ForEach(x => { totalSessionTime += Convert.ToInt64(x.SessionTime); });
            return new SuccessDataResult<long>(totalSessionTime);
        }
    }
}