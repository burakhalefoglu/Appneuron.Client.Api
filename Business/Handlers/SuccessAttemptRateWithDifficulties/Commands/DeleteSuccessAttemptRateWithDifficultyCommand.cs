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

namespace Business.Handlers.SuccessAttemptRateWithDifficulties.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class DeleteSuccessAttemptRateWithDifficultyCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeleteSuccessAttemptRateWithDifficultyCommandHandler : IRequestHandler<DeleteSuccessAttemptRateWithDifficultyCommand, IResult>
        {
            private readonly ISuccessAttemptRateWithDifficultyRepository _successAttemptRateWithDifficultyRepository;
            private readonly IMediator _mediator;

            public DeleteSuccessAttemptRateWithDifficultyCommandHandler(ISuccessAttemptRateWithDifficultyRepository successAttemptRateWithDifficultyRepository, IMediator mediator)
            {
                _successAttemptRateWithDifficultyRepository = successAttemptRateWithDifficultyRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteSuccessAttemptRateWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                await _successAttemptRateWithDifficultyRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}