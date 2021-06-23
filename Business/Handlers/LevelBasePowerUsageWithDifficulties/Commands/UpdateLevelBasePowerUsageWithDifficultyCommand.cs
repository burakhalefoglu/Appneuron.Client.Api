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
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.LevelBasePowerUsageWithDifficulties.Commands
{
    public class UpdateLevelBasePowerUsageWithDifficultyCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public int DifficultyLevel { get; set; }
        public long PowerUsageCount { get; set; }
        public System.DateTime DateTime { get; set; }
        public int LevelIndex { get; set; }

        public class UpdateLevelBasePowerUsageWithDifficultyCommandHandler : IRequestHandler<UpdateLevelBasePowerUsageWithDifficultyCommand, IResult>
        {
            private readonly ILevelBasePowerUsageWithDifficultyRepository _levelBasePowerUsageWithDifficultyRepository;
            private readonly IMediator _mediator;

            public UpdateLevelBasePowerUsageWithDifficultyCommandHandler(ILevelBasePowerUsageWithDifficultyRepository levelBasePowerUsageWithDifficultyRepository, IMediator mediator)
            {
                _levelBasePowerUsageWithDifficultyRepository = levelBasePowerUsageWithDifficultyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateLevelBasePowerUsageWithDifficultyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateLevelBasePowerUsageWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                var levelBasePowerUsageWithDifficulty = new LevelBasePowerUsageWithDifficulty();
                levelBasePowerUsageWithDifficulty.ProjectId = request.ProjectId;
                levelBasePowerUsageWithDifficulty.DifficultyLevel = request.DifficultyLevel;
                levelBasePowerUsageWithDifficulty.PowerUsageCount = request.PowerUsageCount;
                levelBasePowerUsageWithDifficulty.DateTime = request.DateTime;
                levelBasePowerUsageWithDifficulty.LevelIndex = request.LevelIndex;

                await _levelBasePowerUsageWithDifficultyRepository.UpdateAsync(request.Id, levelBasePowerUsageWithDifficulty);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}