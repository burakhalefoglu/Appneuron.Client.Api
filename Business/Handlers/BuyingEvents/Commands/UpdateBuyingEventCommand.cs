using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.BuyingEvents.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.BuyingEvents.Commands
{
    public class UpdateBuyingEventCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ClientId { get; set; }
        public string ProjectID { get; set; }
        public string CustomerID { get; set; }
        public string TrigersInlevelName { get; set; }
        public string ProductType { get; set; }
        public int DifficultyLevel { get; set; }
        public float InWhatMinutes { get; set; }
        public System.DateTime TrigerdTime { get; set; }

        public class UpdateBuyingEventCommandHandler : IRequestHandler<UpdateBuyingEventCommand, IResult>
        {
            private readonly IBuyingEventRepository _buyingEventRepository;
            private readonly IMediator _mediator;

            public UpdateBuyingEventCommandHandler(IBuyingEventRepository buyingEventRepository, IMediator mediator)
            {
                _buyingEventRepository = buyingEventRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateBuyingEventValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateBuyingEventCommand request, CancellationToken cancellationToken)
            {
                var buyingEvent = new BuyingEvent();
                buyingEvent.ClientId = request.ClientId;
                buyingEvent.ProjectID = request.ProjectID;
                buyingEvent.CustomerID = request.CustomerID;
                buyingEvent.TrigersInlevelName = request.TrigersInlevelName;
                buyingEvent.ProductType = request.ProductType;
                buyingEvent.DifficultyLevel = request.DifficultyLevel;
                buyingEvent.InWhatMinutes = request.InWhatMinutes;
                buyingEvent.TrigerdTime = request.TrigerdTime;

                await _buyingEventRepository.UpdateAsync(request.Id, buyingEvent);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}