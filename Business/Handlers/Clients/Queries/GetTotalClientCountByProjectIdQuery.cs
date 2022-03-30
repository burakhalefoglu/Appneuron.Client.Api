using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Clients.Queries;

public class GetTotalClientCountByProjectIdQuery : IRequest<IDataResult<long>>
{
    public long ProjectId { get; set; }

    public class GetTotalClientCountByProjectIdQueryHandler : IRequestHandler<GetTotalClientCountByProjectIdQuery,
        IDataResult<long>>
    {
        private readonly IClientRepository _clientRepository;

        public GetTotalClientCountByProjectIdQueryHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [PerformanceAspect(5)]
        [CacheAspect(10)]
        [LogAspect(typeof(ConsoleLogger))]
        [SecuredOperation(Priority = 1)]
        public async Task<IDataResult<long>>Handle(GetTotalClientCountByProjectIdQuery request,
            CancellationToken cancellationToken)
        {
            var client =
                await _clientRepository.GetListAsync(c => c.ProjectId == request.ProjectId);
            return new SuccessDataResult<long>(client.ToList().Count);
        }
    }
}