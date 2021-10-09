
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Clients.ValidationRules;
using MongoDB.Bson;

namespace Business.Handlers.Clients.Commands
{


    public class UpdateClientCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ClientId { get; set; }
        public string ProjectKey { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public bool IsPaidClient { get; set; }

        public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, IResult>
        {
            private readonly IClientRepository _clientRepository;
            private readonly IMediator _mediator;

            public UpdateClientCommandHandler(IClientRepository clientRepository, IMediator mediator)
            {
                _clientRepository = clientRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateClientValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
            {



                var client = new ClientDataModel();
                client.ClientId = request.ClientId;
                client.ProjectId = request.ProjectKey;
                client.CreatedAt = request.CreatedAt;
                client.IsPaidClient = request.IsPaidClient ? 1 : 0;


                await _clientRepository.UpdateAsync(request.Id, client);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

