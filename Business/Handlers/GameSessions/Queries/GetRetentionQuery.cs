using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.GameSessions.Queries;

public class GetRetentionQuery : IRequest<IDataResult<long[]>>
{
    public long ProjectId { get; set; }

    public DateTimeOffset SessionDate { get; set; }

    public class GetRetentionQueryHandler : IRequestHandler<GetRetentionQuery,
        IDataResult<long[]>>
    {
        private readonly IGameSessionRepository _gameSessionRepository;

        public GetRetentionQueryHandler(IGameSessionRepository gameSessionRepository)
        {
            _gameSessionRepository = gameSessionRepository;
        }

        [PerformanceAspect(5)]
        [CacheAspect(10)]
        [LogAspect(typeof(ConsoleLogger))]
        [SecuredOperation(Priority = 1)]
        public async Task<IDataResult<long[]>> Handle(GetRetentionQuery request,
            CancellationToken cancellationToken)
        {
            var retentions = new List<long>();
            var retentionStrategy = new[]
                {
                    0, 1, 3, 7, 14, 30
                };
            var clientSessions = new List<GameSessionModel>();

            var client =
                await _gameSessionRepository.GetListAsync(c => c.ProjectId == request.ProjectId);
            for (var i = 0; i < retentionStrategy.Length; i++)
            {
                if (i == 0)
                {
                    clientSessions = client.ToList().Where(x
                        => x.SessionStartTime.ToString("MM/dd/yyyy") ==
                           request.SessionDate.ToString("MM/dd/yyyy")).ToList();
                    retentions.Add(100);
                    continue;
                }

                var clientCount = 0;
                clientSessions.ForEach(c =>
                {
                    if (client.ToList().Any(x
                            => x.SessionStartTime.ToString("MM/dd/yyyy") ==
                               request.SessionDate.AddDays(retentionStrategy[i]).ToString("MM/dd/yyyy") &&
                               x.ClientId == c.ClientId))
                    {
                        clientCount++;
                    }  
                });
                var percent = 0;
                    if(clientCount !=0 && clientSessions.Count != 0)
                        percent = 100 * clientCount / clientSessions.Count;
                retentions.Add(percent);
            }

            return new SuccessDataResult<long[]>(retentions.ToArray());
        }
    }
}