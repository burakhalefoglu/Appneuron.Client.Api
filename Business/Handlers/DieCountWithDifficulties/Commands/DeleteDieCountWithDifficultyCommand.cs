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

namespace Business.Handlers.DieCountWithDifficulties.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class DeleteDieCountWithDifficultyCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeleteDieCountWithDifficultyCommandHandler : IRequestHandler<DeleteDieCountWithDifficultyCommand, IResult>
        {
            private readonly IDieCountWithDifficultyRepository _dieCountWithDifficultyRepository;
            private readonly IMediator _mediator;

            public DeleteDieCountWithDifficultyCommandHandler(IDieCountWithDifficultyRepository dieCountWithDifficultyRepository, IMediator mediator)
            {
                _dieCountWithDifficultyRepository = dieCountWithDifficultyRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteDieCountWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                await _dieCountWithDifficultyRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}