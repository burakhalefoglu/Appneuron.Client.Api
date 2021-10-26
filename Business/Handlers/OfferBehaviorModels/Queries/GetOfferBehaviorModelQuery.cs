
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

namespace Business.Handlers.OfferBehaviorModels.Queries
{
    public class GetOfferBehaviorModelQuery : IRequest<IDataResult<OfferBehaviorModel>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetOfferBehaviorModelQueryHandler : IRequestHandler<GetOfferBehaviorModelQuery, IDataResult<OfferBehaviorModel>>
        {
            private readonly IOfferBehaviorModelRepository _offerBehaviorModelRepository;
            private readonly IMediator _mediator;

            public GetOfferBehaviorModelQueryHandler(IOfferBehaviorModelRepository offerBehaviorModelRepository, IMediator mediator)
            {
                _offerBehaviorModelRepository = offerBehaviorModelRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<OfferBehaviorModel>> Handle(GetOfferBehaviorModelQuery request, CancellationToken cancellationToken)
            {
                var offerBehaviorModel = await _offerBehaviorModelRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<OfferBehaviorModel>(offerBehaviorModel);
            }
        }
    }
}
