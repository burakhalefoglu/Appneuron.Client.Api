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
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.BuyingCountWithDifficulties.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class CreateBuyingCountWithDifficultyCommand : IRequest<IResult>
    {
        public string ProjectId { get; set; }
        public int DifficultyLevel { get; set; }
        public long BuyingCount { get; set; }
        public DateTime DateTime { get; set; }

        public class CreateBuyingCountWithDifficultyCommandHandler : IRequestHandler<CreateBuyingCountWithDifficultyCommand, IResult>
        {
            private readonly IBuyingCountWithDifficultyRepository _buyingCountWithDifficultyRepository;
            private readonly IMediator _mediator;

            public CreateBuyingCountWithDifficultyCommandHandler(IBuyingCountWithDifficultyRepository buyingCountWithDifficultyRepository, IMediator mediator)
            {
                _buyingCountWithDifficultyRepository = buyingCountWithDifficultyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateBuyingCountWithDifficultyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateBuyingCountWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                var isThereBuyingCountWithDifficultyRecord = _buyingCountWithDifficultyRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isThereBuyingCountWithDifficultyRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedBuyingCountWithDifficulty = new BuyingCountWithDifficulty
                {
                    ProjectId = request.ProjectId,
                    BuyingCount = request.BuyingCount,
                    DateTime = request.DateTime,
                    DifficultyLevel = request.DifficultyLevel
                };

                await _buyingCountWithDifficultyRepository.AddAsync(addedBuyingCountWithDifficulty);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}