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

namespace Business.Handlers.AdvClickWithDifficulties.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class DeleteAdvClickWithDifficultyCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeleteAdvClickWithDifficultyCommandHandler : IRequestHandler<DeleteAdvClickWithDifficultyCommand, IResult>
        {
            private readonly IAdvClickWithDifficultyRepository _advClickWithDifficultyRepository;
            private readonly IMediator _mediator;

            public DeleteAdvClickWithDifficultyCommandHandler(IAdvClickWithDifficultyRepository advClickWithDifficultyRepository, IMediator mediator)
            {
                _advClickWithDifficultyRepository = advClickWithDifficultyRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteAdvClickWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                await _advClickWithDifficultyRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}