
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
using Business.Handlers.BuyingEvents.ValidationRules;

namespace Business.Handlers.BuyingEvents.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateBuyingEventCommand : IRequest<IResult>
    {

        public string ClientId { get; set; }
        public string ProjectID { get; set; }
        public string CustomerID { get; set; }
        public string TrigersInlevelName { get; set; }
        public string ProductType { get; set; }
        public int DifficultyLevel { get; set; }
        public float InWhatMinutes { get; set; }
        public System.DateTime TrigerdTime { get; set; }


        public class CreateBuyingEventCommandHandler : IRequestHandler<CreateBuyingEventCommand, IResult>
        {
            private readonly IBuyingEventRepository _buyingEventRepository;
            private readonly IMediator _mediator;
            public CreateBuyingEventCommandHandler(IBuyingEventRepository buyingEventRepository, IMediator mediator)
            {
                _buyingEventRepository = buyingEventRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateBuyingEventValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateBuyingEventCommand request, CancellationToken cancellationToken)
            {

                var addedBuyingEvent = new BuyingEvent
                {
                    ClientId = request.ClientId,
                    ProjectID = request.ProjectID,
                    CustomerID = request.CustomerID,
                    TrigersInlevelName = request.TrigersInlevelName,
                    ProductType = request.ProductType,
                    DifficultyLevel = request.DifficultyLevel,
                    InWhatMinutes = request.InWhatMinutes,
                    TrigerdTime = request.TrigerdTime,

                };

                await _buyingEventRepository.AddAsync(addedBuyingEvent);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}