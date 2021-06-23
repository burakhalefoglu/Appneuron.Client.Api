using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.LevelBaseDieCountWithDifficulties.ValidationRules;
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

namespace Business.Handlers.LevelBaseDieCountWithDifficulties.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class CreateLevelBaseDieCountWithDifficultyCommand : IRequest<IResult>
    {
        public string ProjectId { get; set; }
        public int LevelIndex { get; set; }
        public long DieCount { get; set; }
        public System.DateTime DateTime { get; set; }
        public int DifficultyLevel { get; set; }

        public class CreateLevelBaseDieCountWithDifficultyCommandHandler : IRequestHandler<CreateLevelBaseDieCountWithDifficultyCommand, IResult>
        {
            private readonly ILevelBaseDieCountWithDifficultyRepository _levelBaseDieCountWithDifficultyRepository;
            private readonly IMediator _mediator;

            public CreateLevelBaseDieCountWithDifficultyCommandHandler(ILevelBaseDieCountWithDifficultyRepository levelBaseDieCountWithDifficultyRepository, IMediator mediator)
            {
                _levelBaseDieCountWithDifficultyRepository = levelBaseDieCountWithDifficultyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateLevelBaseDieCountWithDifficultyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateLevelBaseDieCountWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                var isThereLevelBaseDieCountWithDifficultyRecord = _levelBaseDieCountWithDifficultyRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isThereLevelBaseDieCountWithDifficultyRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedLevelBaseDieCountWithDifficulty = new LevelBaseDieCountWithDifficulty
                {
                    ProjectId = request.ProjectId,
                    LevelIndex = request.LevelIndex,
                    DieCount = request.DieCount,
                    DateTime = request.DateTime,
                    DifficultyLevel = request.DifficultyLevel,
                };

                await _levelBaseDieCountWithDifficultyRepository.AddAsync(addedLevelBaseDieCountWithDifficulty);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}