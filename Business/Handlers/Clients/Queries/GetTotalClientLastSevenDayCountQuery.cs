namespace Business.Handlers.Clients.Queries;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

public class GetTotalClientLastSevenDayCountQuery: IRequest<IDataResult<long[]>>
{
    public long ProjectId { get; set; }

    public class GetTotalClientLastSevenDayCountQueryHandler : IRequestHandler<GetTotalClientLastSevenDayCountQuery,
        IDataResult<long[]>>
    {
        private readonly IClientRepository _clientRepository;

        public GetTotalClientLastSevenDayCountQueryHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [PerformanceAspect(5)]
        [CacheAspect(10)]
        [LogAspect(typeof(ConsoleLogger))]
        [SecuredOperation(Priority = 1)]
        public async Task<IDataResult<long[]>>Handle(GetTotalClientLastSevenDayCountQuery request,
            CancellationToken cancellationToken)
        {
            var clients = new List<long>();
            var client =
                await _clientRepository.GetListAsync(c => c.ProjectId == request.ProjectId);
            for (var i = 0; i < 7; i++)
            {
                clients.Add(client.Where(x=> x.CreatedAt.Ticks <= DateTime.Now.AddDays(-i).Ticks).ToList().Count);
            }
            return new SuccessDataResult<long[]>(clients.ToArray());
        }
    }
}