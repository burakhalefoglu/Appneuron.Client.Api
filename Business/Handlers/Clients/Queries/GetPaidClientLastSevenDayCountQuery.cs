using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Clients.Queries;

public class GetPaidClientLastSevenDayCountQuery: IRequest<IDataResult<long[]>>
{
    public long ProjectId { get; set; }

    public class GetPaidClientLastSevenDayCountQueryHandler : IRequestHandler<GetPaidClientLastSevenDayCountQuery,
        IDataResult<long[]>>
    {
        private readonly IClientRepository _clientRepository;

        public GetPaidClientLastSevenDayCountQueryHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [PerformanceAspect(5)]
        [CacheAspect(10)]
        [LogAspect(typeof(ConsoleLogger))]
        [SecuredOperation(Priority = 1)]
        public async Task<IDataResult<long[]>>Handle(GetPaidClientLastSevenDayCountQuery request,
            CancellationToken cancellationToken)
        {
            var clients = new List<long>();
            var client =
                await _clientRepository.GetListAsync(c => c.ProjectId == request.ProjectId);
            for (var i = 0; i < 7; i++)
            {
                clients.Add(client.ToList().Where(x=> x.CreatedAt.Ticks < DateTimeOffset.Now.AddDays(-i).Ticks
                && x.IsPaidClient == 1).ToList().Count);
            }
            return new SuccessDataResult<long[]>(clients.ToArray());
        }
    }
}