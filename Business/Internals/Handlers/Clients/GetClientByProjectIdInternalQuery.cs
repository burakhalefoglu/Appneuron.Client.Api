
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using MongoDB.Bson;

namespace Business.Handlers.Clients.Queries
{
    public class GetClientByProjectIdInternalQuery : IRequest<IDataResult<Client>>
    {
        public string ProjectId { get; set; }
        public string ClientId { get; set; }

        public class GetClientByProjectIdInternalQueryHandler : IRequestHandler<GetClientByProjectIdInternalQuery, IDataResult<Client>>
        {
            private readonly IClientRepository _clientRepository;
            private readonly IMediator _mediator;

            public GetClientByProjectIdInternalQueryHandler(IClientRepository clientRepository, IMediator mediator)
            {
                _clientRepository = clientRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<Client>> Handle(GetClientByProjectIdInternalQuery request, CancellationToken cancellationToken)
            {
                var client = await _clientRepository.GetByFilterAsync(c => c.ClientId == request.ClientId && c.ProjectKey == request.ProjectId);
                return new SuccessDataResult<Client>(client);
            }
        }
    }
}
