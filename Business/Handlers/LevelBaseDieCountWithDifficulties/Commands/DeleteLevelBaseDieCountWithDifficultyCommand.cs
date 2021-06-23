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

namespace Business.Handlers.LevelBaseDieCountWithDifficulties.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class DeleteLevelBaseDieCountWithDifficultyCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeleteLevelBaseDieCountWithDifficultyCommandHandler : IRequestHandler<DeleteLevelBaseDieCountWithDifficultyCommand, IResult>
        {
            private readonly ILevelBaseDieCountWithDifficultyRepository _levelBaseDieCountWithDifficultyRepository;
            private readonly IMediator _mediator;

            public DeleteLevelBaseDieCountWithDifficultyCommandHandler(ILevelBaseDieCountWithDifficultyRepository levelBaseDieCountWithDifficultyRepository, IMediator mediator)
            {
                _levelBaseDieCountWithDifficultyRepository = levelBaseDieCountWithDifficultyRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteLevelBaseDieCountWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                await _levelBaseDieCountWithDifficultyRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}