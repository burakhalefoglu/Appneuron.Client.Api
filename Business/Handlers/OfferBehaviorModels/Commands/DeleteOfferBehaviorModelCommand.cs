
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Business.Handlers.OfferBehaviorModels.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOfferBehaviorModelCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeleteOfferBehaviorModelCommandHandler : IRequestHandler<DeleteOfferBehaviorModelCommand, IResult>
        {
            private readonly IOfferBehaviorModelRepository _offerBehaviorModelRepository;
            private readonly IMediator _mediator;

            public DeleteOfferBehaviorModelCommandHandler(IOfferBehaviorModelRepository offerBehaviorModelRepository, IMediator mediator)
            {
                _offerBehaviorModelRepository = offerBehaviorModelRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOfferBehaviorModelCommand request, CancellationToken cancellationToken)
            {


                await _offerBehaviorModelRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

