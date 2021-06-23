using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.LevelBaseSessionWithDifficulties.ValidationRules;
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

namespace Business.Handlers.LevelBaseSessionWithDifficulties.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class CreateLevelBaseSessionWithDifficultyCommand : IRequest<IResult>
    {
        public string ProjectId { get; set; }
        public int LevelIndex { get; set; }
        public int DifficultyLevel { get; set; }
        public int SessionCount { get; set; }
        public float SessionTime { get; set; }

        public class CreateLevelBaseSessionWithDifficultyCommandHandler : IRequestHandler<CreateLevelBaseSessionWithDifficultyCommand, IResult>
        {
            private readonly ILevelBaseSessionWithDifficultyRepository _levelBaseSessionWithDifficultyRepository;
            private readonly IMediator _mediator;

            public CreateLevelBaseSessionWithDifficultyCommandHandler(ILevelBaseSessionWithDifficultyRepository levelBaseSessionWithDifficultyRepository, IMediator mediator)
            {
                _levelBaseSessionWithDifficultyRepository = levelBaseSessionWithDifficultyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateLevelBaseSessionWithDifficultyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateLevelBaseSessionWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                var isThereLevelBaseSessionWithDifficultyRecord = _levelBaseSessionWithDifficultyRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isThereLevelBaseSessionWithDifficultyRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedLevelBaseSessionWithDifficulty = new LevelBaseSessionWithDifficulty
                {
                    ProjectId = request.ProjectId,
                    DifficultyLevel = request.DifficultyLevel,
                    LevelIndex = request.LevelIndex,
                    SessionCount = request.SessionCount,
                    SessionTime = request.SessionTime
                };

                await _levelBaseSessionWithDifficultyRepository.AddAsync(addedLevelBaseSessionWithDifficulty);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}