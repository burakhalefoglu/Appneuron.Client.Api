using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.ChartModels;
using MediatR;
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.LevelBaseFinishingScoreWithDifficulties.Queries
{
    public class GetLevelBaseFinishingScoreWithDifficultyQuery : IRequest<IDataResult<LevelBaseFinishingScoreWithDifficulty>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetLevelBaseFinishingScoreWithDifficultyQueryHandler : IRequestHandler<GetLevelBaseFinishingScoreWithDifficultyQuery, IDataResult<LevelBaseFinishingScoreWithDifficulty>>
        {
            private readonly ILevelBaseFinishingScoreWithDifficultyRepository _levelBaseFinishingScoreWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetLevelBaseFinishingScoreWithDifficultyQueryHandler(ILevelBaseFinishingScoreWithDifficultyRepository levelBaseFinishingScoreWithDifficultyRepository, IMediator mediator)
            {
                _levelBaseFinishingScoreWithDifficultyRepository = levelBaseFinishingScoreWithDifficultyRepository;
                _mediator = mediator;
            }

            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<LevelBaseFinishingScoreWithDifficulty>> Handle(GetLevelBaseFinishingScoreWithDifficultyQuery request, CancellationToken cancellationToken)
            {
                var levelBaseFinishingScoreWithDifficulty = await _levelBaseFinishingScoreWithDifficultyRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<LevelBaseFinishingScoreWithDifficulty>(levelBaseFinishingScoreWithDifficulty);
            }
        }
    }
}