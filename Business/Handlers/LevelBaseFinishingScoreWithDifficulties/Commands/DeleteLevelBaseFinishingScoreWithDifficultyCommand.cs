using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.LevelBaseFinishingScoreWithDifficulties.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class DeleteLevelBaseFinishingScoreWithDifficultyCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeleteLevelBaseFinishingScoreWithDifficultyCommandHandler : IRequestHandler<DeleteLevelBaseFinishingScoreWithDifficultyCommand, IResult>
        {
            private readonly ILevelBaseFinishingScoreWithDifficultyRepository _levelBaseFinishingScoreWithDifficultyRepository;
            private readonly IMediator _mediator;

            public DeleteLevelBaseFinishingScoreWithDifficultyCommandHandler(ILevelBaseFinishingScoreWithDifficultyRepository levelBaseFinishingScoreWithDifficultyRepository, IMediator mediator)
            {
                _levelBaseFinishingScoreWithDifficultyRepository = levelBaseFinishingScoreWithDifficultyRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteLevelBaseFinishingScoreWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                await _levelBaseFinishingScoreWithDifficultyRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}