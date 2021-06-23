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
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.LevelBasePlayerCountWithDifficulties.Commands
{
    public class UpdateLevelBasePlayerCountWithDifficultyCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public int LevelIndex { get; set; }
        public System.DateTime DateTime { get; set; }
        public long PlayerCount { get; set; }
        public int DifficultyLevel { get; set; }

        public class UpdateLevelBasePlayerCountWithDifficultyCommandHandler : IRequestHandler<UpdateLevelBasePlayerCountWithDifficultyCommand, IResult>
        {
            private readonly ILevelBasePlayerCountWithDifficultyRepository _levelBasePlayerCountWithDifficultyRepository;
            private readonly IMediator _mediator;

            public UpdateLevelBasePlayerCountWithDifficultyCommandHandler(ILevelBasePlayerCountWithDifficultyRepository levelBasePlayerCountWithDifficultyRepository, IMediator mediator)
            {
                _levelBasePlayerCountWithDifficultyRepository = levelBasePlayerCountWithDifficultyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateLevelBasePlayerCountWithDifficultyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateLevelBasePlayerCountWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                var levelBasePlayerCountWithDifficulty = new LevelBasePlayerCountWithDifficulty();
                levelBasePlayerCountWithDifficulty.ProjectId = request.ProjectId;
                levelBasePlayerCountWithDifficulty.LevelIndex = request.LevelIndex;
                levelBasePlayerCountWithDifficulty.DateTime = request.DateTime;
                levelBasePlayerCountWithDifficulty.PlayerCount = request.PlayerCount;
                levelBasePlayerCountWithDifficulty.DifficultyLevel = request.DifficultyLevel;

                await _levelBasePlayerCountWithDifficultyRepository.UpdateAsync(request.Id, levelBasePlayerCountWithDifficulty);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}