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

namespace Business.Handlers.LevelBaseSessionWithDifficulties.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class DeleteLevelBaseSessionWithDifficultyCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeleteLevelBaseSessionWithDifficultyCommandHandler : IRequestHandler<DeleteLevelBaseSessionWithDifficultyCommand, IResult>
        {
            private readonly ILevelBaseSessionWithDifficultyRepository _levelBaseSessionWithDifficultyRepository;
            private readonly IMediator _mediator;

            public DeleteLevelBaseSessionWithDifficultyCommandHandler(ILevelBaseSessionWithDifficultyRepository levelBaseSessionWithDifficultyRepository, IMediator mediator)
            {
                _levelBaseSessionWithDifficultyRepository = levelBaseSessionWithDifficultyRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteLevelBaseSessionWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                await _levelBaseSessionWithDifficultyRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}