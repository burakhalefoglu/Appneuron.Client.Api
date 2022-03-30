using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Clients.Queries;

public class GetPaidClientCountByProjectIdQuery: IRequest<IDataResult<long>>
{
    public long ProjectId { get; set; }

    public class GetPaidClientCountByProjectIdQueryHandler : IRequestHandler<GetPaidClientCountByProjectIdQuery,
        IDataResult<long>>
    {
        private readonly IClientRepository _clientRepository;

        public GetPaidClientCountByProjectIdQueryHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [PerformanceAspect(5)]
        [CacheAspect(10)]
        [LogAspect(typeof(ConsoleLogger))]
        [SecuredOperation(Priority = 1)]
        public async Task<IDataResult<long>>Handle(GetPaidClientCountByProjectIdQuery request,
            CancellationToken cancellationToken)
        {
            var client =
                (await _clientRepository.GetListAsync(c => c.ProjectId == request.ProjectId)).Where(
                    x=> x.IsPaidClient == 1);
            return new SuccessDataResult<long>(client.ToList().Count);
        }
    }
}