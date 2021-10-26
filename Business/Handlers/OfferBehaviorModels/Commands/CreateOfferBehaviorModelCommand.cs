
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
using System.Linq;
using Business.Handlers.OfferBehaviorModels.ValidationRules;

namespace Business.Handlers.OfferBehaviorModels.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOfferBehaviorModelCommand : IRequest<IResult>
    {

        public string ClientId { get; set; }
        public string ProjectId { get; set; }
        public string CustomerId { get; set; }
        public int OfferId { get; set; }
        public string OfferName { get; set; }
        public System.DateTime dateTime { get; set; }
        public int IsBuyOffer { get; set; }


        public class CreateOfferBehaviorModelCommandHandler : IRequestHandler<CreateOfferBehaviorModelCommand, IResult>
        {
            private readonly IOfferBehaviorModelRepository _offerBehaviorModelRepository;
            private readonly IMediator _mediator;
            public CreateOfferBehaviorModelCommandHandler(IOfferBehaviorModelRepository offerBehaviorModelRepository, IMediator mediator)
            {
                _offerBehaviorModelRepository = offerBehaviorModelRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOfferBehaviorModelValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOfferBehaviorModelCommand request, CancellationToken cancellationToken)
            {
                var isThereOfferBehaviorModelRecord = _offerBehaviorModelRepository.Any(u => u.ClientId == request.ClientId);

                if (isThereOfferBehaviorModelRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOfferBehaviorModel = new OfferBehaviorModel
                {
                    ClientId = request.ClientId,
                    ProjectId = request.ProjectId,
                    CustomerId = request.CustomerId,
                    Version = request.OfferId,
                    OfferName = request.OfferName,
                    dateTime = request.dateTime,
                    IsBuyOffer = request.IsBuyOffer,

                };

                await _offerBehaviorModelRepository.AddAsync(addedOfferBehaviorModel);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}