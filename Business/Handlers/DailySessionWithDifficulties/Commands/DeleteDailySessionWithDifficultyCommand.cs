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

namespace Business.Handlers.DailySessionWithDifficulties.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class DeleteDailySessionWithDifficultyCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeleteDailySessionWithDifficultyCommandHandler : IRequestHandler<DeleteDailySessionWithDifficultyCommand, IResult>
        {
            private readonly IDailySessionWithDifficultyRepository _dailySessionWithDifficultyRepository;
            private readonly IMediator _mediator;

            public DeleteDailySessionWithDifficultyCommandHandler(IDailySessionWithDifficultyRepository dailySessionWithDifficultyRepository, IMediator mediator)
            {
                _dailySessionWithDifficultyRepository = dailySessionWithDifficultyRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteDailySessionWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                await _dailySessionWithDifficultyRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}