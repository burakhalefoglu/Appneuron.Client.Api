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
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.LevelBaseFinishingScoreWithDifficulties.Commands
{
    public class UpdateLevelBaseFinishingScoreWithDifficultyCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public int LevelIndex { get; set; }
        public long AvarageScore { get; set; }
        public int DifficultyLevel { get; set; }
        public System.DateTime DateTime { get; set; }

        public class UpdateLevelBaseFinishingScoreWithDifficultyCommandHandler : IRequestHandler<UpdateLevelBaseFinishingScoreWithDifficultyCommand, IResult>
        {
            private readonly ILevelBaseFinishingScoreWithDifficultyRepository _levelBaseFinishingScoreWithDifficultyRepository;
            private readonly IMediator _mediator;

            public UpdateLevelBaseFinishingScoreWithDifficultyCommandHandler(ILevelBaseFinishingScoreWithDifficultyRepository levelBaseFinishingScoreWithDifficultyRepository, IMediator mediator)
            {
                _levelBaseFinishingScoreWithDifficultyRepository = levelBaseFinishingScoreWithDifficultyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateLevelBaseFinishingScoreWithDifficultyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateLevelBaseFinishingScoreWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                var levelBaseFinishingScoreWithDifficulty = new LevelBaseFinishingScoreWithDifficulty();
                levelBaseFinishingScoreWithDifficulty.ProjectId = request.ProjectId;
                levelBaseFinishingScoreWithDifficulty.LevelIndex = request.LevelIndex;
                levelBaseFinishingScoreWithDifficulty.AvarageScore = request.AvarageScore;
                levelBaseFinishingScoreWithDifficulty.DifficultyLevel = request.DifficultyLevel;
                levelBaseFinishingScoreWithDifficulty.DateTime = request.DateTime;

                await _levelBaseFinishingScoreWithDifficultyRepository.UpdateAsync(request.Id, levelBaseFinishingScoreWithDifficulty);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}