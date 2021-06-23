using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.LevelBasePlayerCountWithDifficulties.ValidationRules;
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

namespace Business.Handlers.LevelBasePlayerCountWithDifficulties.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class CreateLevelBasePlayerCountWithDifficultyCommand : IRequest<IResult>
    {
        public string ProjectId { get; set; }
        public int LevelIndex { get; set; }
        public System.DateTime DateTime { get; set; }
        public long PlayerCount { get; set; }
        public int DifficultyLevel { get; set; }

        public class CreateLevelBasePlayerCountWithDifficultyCommandHandler : IRequestHandler<CreateLevelBasePlayerCountWithDifficultyCommand, IResult>
        {
            private readonly ILevelBasePlayerCountWithDifficultyRepository _levelBasePlayerCountWithDifficultyRepository;
            private readonly IMediator _mediator;

            public CreateLevelBasePlayerCountWithDifficultyCommandHandler(ILevelBasePlayerCountWithDifficultyRepository levelBasePlayerCountWithDifficultyRepository, IMediator mediator)
            {
                _levelBasePlayerCountWithDifficultyRepository = levelBasePlayerCountWithDifficultyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateLevelBasePlayerCountWithDifficultyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateLevelBasePlayerCountWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                var isThereLevelBasePlayerCountWithDifficultyRecord = _levelBasePlayerCountWithDifficultyRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isThereLevelBasePlayerCountWithDifficultyRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedLevelBasePlayerCountWithDifficulty = new LevelBasePlayerCountWithDifficulty
                {
                    ProjectId = request.ProjectId,
                    LevelIndex = request.LevelIndex,
                    DateTime = request.DateTime,
                    PlayerCount = request.PlayerCount,
                    DifficultyLevel = request.DifficultyLevel,
                };

                await _levelBasePlayerCountWithDifficultyRepository.AddAsync(addedLevelBasePlayerCountWithDifficulty);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}