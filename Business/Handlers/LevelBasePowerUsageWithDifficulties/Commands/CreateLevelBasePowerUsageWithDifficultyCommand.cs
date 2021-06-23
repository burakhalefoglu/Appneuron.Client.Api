using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.LevelBasePowerUsageWithDifficulties.ValidationRules;
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

namespace Business.Handlers.LevelBasePowerUsageWithDifficulties.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class CreateLevelBasePowerUsageWithDifficultyCommand : IRequest<IResult>
    {
        public string ProjectId { get; set; }
        public int DifficultyLevel { get; set; }
        public long PowerUsageCount { get; set; }
        public System.DateTime DateTime { get; set; }
        public int LevelIndex { get; set; }

        public class CreateLevelBasePowerUsageWithDifficultyCommandHandler : IRequestHandler<CreateLevelBasePowerUsageWithDifficultyCommand, IResult>
        {
            private readonly ILevelBasePowerUsageWithDifficultyRepository _levelBasePowerUsageWithDifficultyRepository;
            private readonly IMediator _mediator;

            public CreateLevelBasePowerUsageWithDifficultyCommandHandler(ILevelBasePowerUsageWithDifficultyRepository levelBasePowerUsageWithDifficultyRepository, IMediator mediator)
            {
                _levelBasePowerUsageWithDifficultyRepository = levelBasePowerUsageWithDifficultyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateLevelBasePowerUsageWithDifficultyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateLevelBasePowerUsageWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                var isThereLevelBasePowerUsageWithDifficultyRecord = _levelBasePowerUsageWithDifficultyRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isThereLevelBasePowerUsageWithDifficultyRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedLevelBasePowerUsageWithDifficulty = new LevelBasePowerUsageWithDifficulty
                {
                    ProjectId = request.ProjectId,
                    DifficultyLevel = request.DifficultyLevel,
                    PowerUsageCount = request.PowerUsageCount,
                    DateTime = request.DateTime,
                    LevelIndex = request.LevelIndex,
                };

                await _levelBasePowerUsageWithDifficultyRepository.AddAsync(addedLevelBasePowerUsageWithDifficulty);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}