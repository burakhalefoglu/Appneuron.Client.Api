using System.Threading;
using System.Threading.Tasks;
using Business.Constants;
using Business.Handlers.Clients.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Internals.Handlers.Clients
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateClientInternalCommand : IRequest<IResult>
    {

        public long ClientId { get; set; }
        public long ProjectId { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public byte IsPaidClient { get; set; }


        public class CreateClientInternalCommandHandler : IRequestHandler<CreateClientInternalCommand, IResult>
        {
            private readonly IClientRepository _clientRepository;
            private readonly IMediator _mediator;
            public CreateClientInternalCommandHandler(IClientRepository clientRepository, IMediator mediator)
            {
                _clientRepository = clientRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateClientValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateClientInternalCommand request, CancellationToken cancellationToken)
            {
                var isThereClientRecord = _clientRepository.Any(u => u.Id == request.ClientId);

                if (isThereClientRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedClient = new ClientDataModel
                {
                    Id = request.ClientId,
                    ProjectId = request.ProjectId,
                    CreatedAt = request.CreatedAt,
                    IsPaidClient = request.IsPaidClient,

                };

                await _clientRepository.AddAsync(addedClient);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}