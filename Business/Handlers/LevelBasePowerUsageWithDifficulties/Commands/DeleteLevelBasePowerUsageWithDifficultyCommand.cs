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

namespace Business.Handlers.LevelBasePowerUsageWithDifficulties.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class DeleteLevelBasePowerUsageWithDifficultyCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeleteLevelBasePowerUsageWithDifficultyCommandHandler : IRequestHandler<DeleteLevelBasePowerUsageWithDifficultyCommand, IResult>
        {
            private readonly ILevelBasePowerUsageWithDifficultyRepository _levelBasePowerUsageWithDifficultyRepository;
            private readonly IMediator _mediator;

            public DeleteLevelBasePowerUsageWithDifficultyCommandHandler(ILevelBasePowerUsageWithDifficultyRepository levelBasePowerUsageWithDifficultyRepository, IMediator mediator)
            {
                _levelBasePowerUsageWithDifficultyRepository = levelBasePowerUsageWithDifficultyRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteLevelBasePowerUsageWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                await _levelBasePowerUsageWithDifficultyRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}