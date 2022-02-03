
using Business.BusinessAspects;
using Core.Utilities.Results;
using Core.Aspects.Autofac.Performance;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.Clients.Queries
{

    public class GetClientsByProjectIdQuery : IRequest<IDataResult<IEnumerable<ClientDataModel>>>
    {
        public string ProjectId { get; set; }

        public class GetClientsByProjectIdQueryHandler : IRequestHandler<GetClientsByProjectIdQuery, IDataResult<IEnumerable<ClientDataModel>>>
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
            public async Task<IDataResult<IEnumerable<ClientDataModel>>> Handle(GetClientsByProjectIdQuery request, CancellationToken cancellationToken)
            {
                var client = await _clientRepository.GetListAsync(c => c.ProjectId == request.ProjectId);
                return new SuccessDataResult<IEnumerable<ClientDataModel>>(client);
            }
        }
    }
}