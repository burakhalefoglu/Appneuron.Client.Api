
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.Clients.Queries
{

    public class GetClientsQuery : IRequest<IDataResult<IEnumerable<ClientDataModel>>>
    {
        public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, IDataResult<IEnumerable<ClientDataModel>>>
        {
            private readonly IClientRepository _clientRepository;
            private readonly IMediator _mediator;

            public GetClientsQueryHandler(IClientRepository clientRepository, IMediator mediator)
            {
                _clientRepository = clientRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ClientDataModel>>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ClientDataModel>>(await _clientRepository.GetListAsync());
            }
        }
    }
}