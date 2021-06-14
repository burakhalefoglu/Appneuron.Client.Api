
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

namespace Business.Handlers.BuyingEvents.Queries
{
    public class GetBuyingEventQuery : IRequest<IDataResult<BuyingEvent>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetBuyingEventQueryHandler : IRequestHandler<GetBuyingEventQuery, IDataResult<BuyingEvent>>
        {
            private readonly IBuyingEventRepository _buyingEventRepository;
            private readonly IMediator _mediator;

            public GetBuyingEventQueryHandler(IBuyingEventRepository buyingEventRepository, IMediator mediator)
            {
                _buyingEventRepository = buyingEventRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<BuyingEvent>> Handle(GetBuyingEventQuery request, CancellationToken cancellationToken)
            {
                var buyingEvent = await _buyingEventRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<BuyingEvent>(buyingEvent);
            }
        }
    }
}
