using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.BuyingCountWithDifficulties.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.ChartModels;
using MediatR;
using MongoDB.Bson;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.BuyingCountWithDifficulties.Commands
{
    public class UpdateBuyingCountWithDifficultyCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public int DifficultyLevel { get; set; }
        public long BuyingCount { get; set; }
        public DateTime DateTime { get; set; }

        public class UpdateBuyingCountWithDifficultyCommandHandler : IRequestHandler<UpdateBuyingCountWithDifficultyCommand, IResult>
        {
            private readonly IBuyingCountWithDifficultyRepository _buyingCountWithDifficultyRepository;
            private readonly IMediator _mediator;

            public UpdateBuyingCountWithDifficultyCommandHandler(IBuyingCountWithDifficultyRepository buyingCountWithDifficultyRepository, IMediator mediator)
            {
                _buyingCountWithDifficultyRepository = buyingCountWithDifficultyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateBuyingCountWithDifficultyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateBuyingCountWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                var buyingCountWithDifficulty = new BuyingCountWithDifficulty();
                buyingCountWithDifficulty.ProjectId = request.ProjectId;
                buyingCountWithDifficulty.BuyingCount = request.BuyingCount;
                buyingCountWithDifficulty.DateTime = request.DateTime;
                buyingCountWithDifficulty.DifficultyLevel = request.DifficultyLevel;

                await _buyingCountWithDifficultyRepository.UpdateAsync(request.Id, buyingCountWithDifficulty);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}