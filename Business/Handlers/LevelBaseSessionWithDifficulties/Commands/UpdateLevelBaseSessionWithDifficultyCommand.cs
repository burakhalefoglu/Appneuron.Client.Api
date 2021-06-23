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
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.LevelBaseSessionWithDifficulties.Commands
{
    public class UpdateLevelBaseSessionWithDifficultyCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public int LevelIndex { get; set; }
        public int DifficultyLevel { get; set; }
        public int SessionCount { get; set; }
        public float SessionTime { get; set; }

        public class UpdateLevelBaseSessionWithDifficultyCommandHandler : IRequestHandler<UpdateLevelBaseSessionWithDifficultyCommand, IResult>
        {
            private readonly ILevelBaseSessionWithDifficultyRepository _levelBaseSessionWithDifficultyRepository;
            private readonly IMediator _mediator;

            public UpdateLevelBaseSessionWithDifficultyCommandHandler(ILevelBaseSessionWithDifficultyRepository levelBaseSessionWithDifficultyRepository, IMediator mediator)
            {
                _levelBaseSessionWithDifficultyRepository = levelBaseSessionWithDifficultyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateLevelBaseSessionWithDifficultyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateLevelBaseSessionWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                var levelBaseSessionWithDifficulty = new LevelBaseSessionWithDifficulty();
                levelBaseSessionWithDifficulty.ProjectId = request.ProjectId;
                levelBaseSessionWithDifficulty.DifficultyLevel = request.DifficultyLevel;
                levelBaseSessionWithDifficulty.LevelIndex = request.LevelIndex;
                levelBaseSessionWithDifficulty.SessionCount = request.SessionCount;
                levelBaseSessionWithDifficulty.SessionTime = request.SessionTime;

                await _levelBaseSessionWithDifficultyRepository.UpdateAsync(request.Id, levelBaseSessionWithDifficulty);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}