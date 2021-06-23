using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.BuyingCountWithDifficulties.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class DeleteBuyingCountWithDifficultyCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeleteBuyingCountWithDifficultyCommandHandler : IRequestHandler<DeleteBuyingCountWithDifficultyCommand, IResult>
        {
            private readonly IBuyingCountWithDifficultyRepository _buyingCountWithDifficultyRepository;
            private readonly IMediator _mediator;

            public DeleteBuyingCountWithDifficultyCommandHandler(IBuyingCountWithDifficultyRepository buyingCountWithDifficultyRepository, IMediator mediator)
            {
                _buyingCountWithDifficultyRepository = buyingCountWithDifficultyRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteBuyingCountWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                await _buyingCountWithDifficultyRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}