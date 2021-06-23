using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.ChartModels;
using MediatR;
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.BuyingCountWithDifficulties.Queries
{
    public class GetBuyingCountWithDifficultyQuery : IRequest<IDataResult<BuyingCountWithDifficulty>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetBuyingCountWithDifficultyQueryHandler : IRequestHandler<GetBuyingCountWithDifficultyQuery, IDataResult<BuyingCountWithDifficulty>>
        {
            private readonly IBuyingCountWithDifficultyRepository _buyingCountWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetBuyingCountWithDifficultyQueryHandler(IBuyingCountWithDifficultyRepository buyingCountWithDifficultyRepository, IMediator mediator)
            {
                _buyingCountWithDifficultyRepository = buyingCountWithDifficultyRepository;
                _mediator = mediator;
            }

            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<BuyingCountWithDifficulty>> Handle(GetBuyingCountWithDifficultyQuery request, CancellationToken cancellationToken)
            {
                var buyingCountWithDifficulty = await _buyingCountWithDifficultyRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<BuyingCountWithDifficulty>(buyingCountWithDifficulty);
            }
        }
    }
}