using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.Clients.Queries
{
    public class GetClientsByProjectIdQuery : IRequest<IDataResult<IEnumerable<ClientDataModel>>>
    {
        public long ProjectId { get; set; }

        public class GetClientsByProjectIdQueryHandler : IRequestHandler<GetClientsByProjectIdQuery,
            IDataResult<IEnumerable<ClientDataModel>>>
        {
            private readonly IClientRepository _clientRepository;
            private readonly IMediator _mediator;

            public GetClientsByProjectIdQueryHandler(IClientRepository clientRepository, IMediator mediator)
            {
                _clientRepository = clientRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ClientDataModel>>> Handle(GetClientsByProjectIdQuery request,
                CancellationToken cancellationToken)
            {
                var client =
                    await _clientRepository.GetListAsync(c => c.ProjectId == request.ProjectId && c.Status == true);
                return new SuccessDataResult<IEnumerable<ClientDataModel>>(client);
            }
        }
    }
}