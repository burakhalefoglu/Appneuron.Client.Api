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

namespace Business.Handlers.LevelBasePlayerCountWithDifficulties.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class DeleteLevelBasePlayerCountWithDifficultyCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeleteLevelBasePlayerCountWithDifficultyCommandHandler : IRequestHandler<DeleteLevelBasePlayerCountWithDifficultyCommand, IResult>
        {
            private readonly ILevelBasePlayerCountWithDifficultyRepository _levelBasePlayerCountWithDifficultyRepository;
            private readonly IMediator _mediator;

            public DeleteLevelBasePlayerCountWithDifficultyCommandHandler(ILevelBasePlayerCountWithDifficultyRepository levelBasePlayerCountWithDifficultyRepository, IMediator mediator)
            {
                _levelBasePlayerCountWithDifficultyRepository = levelBasePlayerCountWithDifficultyRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteLevelBasePlayerCountWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                await _levelBasePlayerCountWithDifficultyRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}