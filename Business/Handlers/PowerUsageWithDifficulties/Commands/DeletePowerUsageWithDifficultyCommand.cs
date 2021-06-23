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

namespace Business.Handlers.PowerUsageWithDifficulties.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class DeletePowerUsageWithDifficultyCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeletePowerUsageWithDifficultyCommandHandler : IRequestHandler<DeletePowerUsageWithDifficultyCommand, IResult>
        {
            private readonly IPowerUsageWithDifficultyRepository _powerUsageWithDifficultyRepository;
            private readonly IMediator _mediator;

            public DeletePowerUsageWithDifficultyCommandHandler(IPowerUsageWithDifficultyRepository powerUsageWithDifficultyRepository, IMediator mediator)
            {
                _powerUsageWithDifficultyRepository = powerUsageWithDifficultyRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeletePowerUsageWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                await _powerUsageWithDifficultyRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}