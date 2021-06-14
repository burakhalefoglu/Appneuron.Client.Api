
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

namespace Business.Handlers.BuyingEvents.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteBuyingEventCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeleteBuyingEventCommandHandler : IRequestHandler<DeleteBuyingEventCommand, IResult>
        {
            private readonly IBuyingEventRepository _buyingEventRepository;
            private readonly IMediator _mediator;

            public DeleteBuyingEventCommandHandler(IBuyingEventRepository buyingEventRepository, IMediator mediator)
            {
                _buyingEventRepository = buyingEventRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteBuyingEventCommand request, CancellationToken cancellationToken)
            {


                await _buyingEventRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

