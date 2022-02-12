using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Internals.Handlers.Clients
{
    public class GetClientByProjectIdInternalQuery : IRequest<IDataResult<ClientDataModel>>
    {
        public long ProjectId { get; set; }
        public long ClientId { get; set; }

        public class
            GetClientByProjectIdInternalQueryHandler : IRequestHandler<GetClientByProjectIdInternalQuery,
                IDataResult<ClientDataModel>>
        {
            private readonly IClientRepository _clientRepository;
            private readonly IMediator _mediator;

            public GetClientByProjectIdInternalQueryHandler(IClientRepository clientRepository, IMediator mediator)
            {
                _clientRepository = clientRepository;
                _mediator = mediator;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<ClientDataModel>> Handle(GetClientByProjectIdInternalQuery request,
                CancellationToken cancellationToken)
            {
                var client = await _clientRepository.GetAsync(c =>
                    c.Id == request.ClientId && c.ProjectId == request.ProjectId && c.Status == true);
                return new SuccessDataResult<ClientDataModel>(client);
            }
        }
    }
}