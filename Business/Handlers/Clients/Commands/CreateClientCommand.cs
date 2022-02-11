
using System;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

using Business.Handlers.Clients.ValidationRules;

namespace Business.Handlers.Clients.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateClientCommand : IRequest<IResult>
    {

        public long ProjectId { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public bool IsPaidClient { get; set; }


        public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, IResult>
        {
            private readonly IClientRepository _clientRepository;
            private readonly IMediator _mediator;
            public CreateClientCommandHandler(IClientRepository clientRepository, IMediator mediator)
            {
                _clientRepository = clientRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateClientValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateClientCommand request, CancellationToken cancellationToken)
            {

                var addedClient = new ClientDataModel
                {
                    ProjectId = request.ProjectId,
                    CreatedAt = request.CreatedAt,
                    IsPaidClient = Convert.ToByte(request.IsPaidClient ? 1 : 0),
                };

                await _clientRepository.AddAsync(addedClient);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}