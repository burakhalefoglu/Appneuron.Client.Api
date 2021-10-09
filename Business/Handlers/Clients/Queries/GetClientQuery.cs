
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
    public class GetClientQuery : IRequest<IDataResult<ClientDataModel>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetClientQueryHandler : IRequestHandler<GetClientQuery, IDataResult<ClientDataModel>>
        {
            private readonly IClientRepository _clientRepository;
            private readonly IMediator _mediator;

            public GetClientQueryHandler(IClientRepository clientRepository, IMediator mediator)
            {
                _clientRepository = clientRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ClientDataModel>> Handle(GetClientQuery request, CancellationToken cancellationToken)
            {
                var client = await _clientRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<ClientDataModel>(client);
            }
        }
    }
}
