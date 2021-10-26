
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
using Business.Handlers.OfferBehaviorModels.ValidationRules;
using MongoDB.Bson;

namespace Business.Handlers.OfferBehaviorModels.Commands
{


    public class UpdateOfferBehaviorModelCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ClientId { get; set; }
        public string ProjectId { get; set; }
        public string CustomerId { get; set; }
        public int OfferId { get; set; }
        public string OfferName { get; set; }
        public System.DateTime dateTime { get; set; }
        public int IsBuyOffer { get; set; }

        public class UpdateOfferBehaviorModelCommandHandler : IRequestHandler<UpdateOfferBehaviorModelCommand, IResult>
        {
            private readonly IOfferBehaviorModelRepository _offerBehaviorModelRepository;
            private readonly IMediator _mediator;

            public UpdateOfferBehaviorModelCommandHandler(IOfferBehaviorModelRepository offerBehaviorModelRepository, IMediator mediator)
            {
                _offerBehaviorModelRepository = offerBehaviorModelRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOfferBehaviorModelValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOfferBehaviorModelCommand request, CancellationToken cancellationToken)
            {



                var offerBehaviorModel = new OfferBehaviorModel();
                offerBehaviorModel.ClientId = request.ClientId;
                offerBehaviorModel.ProjectId = request.ProjectId;
                offerBehaviorModel.CustomerId = request.CustomerId;
                offerBehaviorModel.Version = request.OfferId;
                offerBehaviorModel.OfferName = request.OfferName;
                offerBehaviorModel.dateTime = request.dateTime;
                offerBehaviorModel.IsBuyOffer = request.IsBuyOffer;


                await _offerBehaviorModelRepository.UpdateAsync(request.Id, offerBehaviorModel);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

