using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.SuccessAttemptRateWithDifficulties.ValidationRules;
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

namespace Business.Handlers.SuccessAttemptRateWithDifficulties.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class CreateSuccessAttemptRateWithDifficultyCommand : IRequest<IResult>
    {
        public string ProjectId { get; set; }
        public int DifficultyLevel { get; set; }
        public long SuccessAttempt { get; set; }
        public DateTime DateTime { get; set; }

        public class CreateSuccessAttemptRateWithDifficultyCommandHandler : IRequestHandler<CreateSuccessAttemptRateWithDifficultyCommand, IResult>
        {
            private readonly ISuccessAttemptRateWithDifficultyRepository _successAttemptRateWithDifficultyRepository;
            private readonly IMediator _mediator;

            public CreateSuccessAttemptRateWithDifficultyCommandHandler(ISuccessAttemptRateWithDifficultyRepository successAttemptRateWithDifficultyRepository, IMediator mediator)
            {
                _successAttemptRateWithDifficultyRepository = successAttemptRateWithDifficultyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateSuccessAttemptRateWithDifficultyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateSuccessAttemptRateWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                var isThereSuccessAttemptRateWithDifficultyRecord = _successAttemptRateWithDifficultyRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isThereSuccessAttemptRateWithDifficultyRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedSuccessAttemptRateWithDifficulty = new SuccessAttemptRateWithDifficulty
                {
                    ProjectId = request.ProjectId,
                    DateTime = request.DateTime,
                    DifficultyLevel = request.DifficultyLevel,
                    SuccessAttempt = request.SuccessAttempt
                };

                await _successAttemptRateWithDifficultyRepository.AddAsync(addedSuccessAttemptRateWithDifficulty);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}