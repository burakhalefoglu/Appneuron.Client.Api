using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.LevelBaseFinishingScoreWithDifficulties.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.ChartModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.LevelBaseFinishingScoreWithDifficulties.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class CreateLevelBaseFinishingScoreWithDifficultyCommand : IRequest<IResult>
    {
        public string ProjectId { get; set; }
        public int LevelIndex { get; set; }
        public long AvarageScore { get; set; }
        public int DifficultyLevel { get; set; }
        public System.DateTime DateTime { get; set; }

        public class CreateLevelBaseFinishingScoreWithDifficultyCommandHandler : IRequestHandler<CreateLevelBaseFinishingScoreWithDifficultyCommand, IResult>
        {
            private readonly ILevelBaseFinishingScoreWithDifficultyRepository _levelBaseFinishingScoreWithDifficultyRepository;
            private readonly IMediator _mediator;

            public CreateLevelBaseFinishingScoreWithDifficultyCommandHandler(ILevelBaseFinishingScoreWithDifficultyRepository levelBaseFinishingScoreWithDifficultyRepository, IMediator mediator)
            {
                _levelBaseFinishingScoreWithDifficultyRepository = levelBaseFinishingScoreWithDifficultyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateLevelBaseFinishingScoreWithDifficultyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateLevelBaseFinishingScoreWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                var isThereLevelBaseFinishingScoreWithDifficultyRecord = _levelBaseFinishingScoreWithDifficultyRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isThereLevelBaseFinishingScoreWithDifficultyRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedLevelBaseFinishingScoreWithDifficulty = new LevelBaseFinishingScoreWithDifficulty
                {
                    ProjectId = request.ProjectId,
                    LevelIndex = request.LevelIndex,
                    AvarageScore = request.AvarageScore,
                    DifficultyLevel = request.DifficultyLevel,
                    DateTime = request.DateTime,
                };

                await _levelBaseFinishingScoreWithDifficultyRepository.AddAsync(addedLevelBaseFinishingScoreWithDifficulty);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}