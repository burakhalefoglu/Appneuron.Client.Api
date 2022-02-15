using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.GameSessionModels.Commands
{
    /// <summary>
    /// </summary>
    public class DeleteGameSessionModelByProjectIdCommand : IRequest<IResult>
    {
        public long ProjectId { get; set; }

        public class
            DeleteGameSessionModelByProjectIdCommandHandler : IRequestHandler<
                DeleteGameSessionModelByProjectIdCommand, IResult>
        {
            private readonly IGameSessionModelRepository _gameSessionEveryLoginDataRepository;
            private readonly IMediator _mediator;

            public DeleteGameSessionModelByProjectIdCommandHandler(
                IGameSessionModelRepository gameSessionEveryLoginDataRepository, IMediator mediator)
            {
                _gameSessionEveryLoginDataRepository = gameSessionEveryLoginDataRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteGameSessionModelByProjectIdCommand request,
                CancellationToken cancellationToken)
            {
                var result =
                    await _gameSessionEveryLoginDataRepository.GetAsync(p =>
                        p.ProjectId == request.ProjectId && p.Status == true);
                if (result is null)
                    return new ErrorResult(Messages.NotFound);
                result.Status = false;
                await _gameSessionEveryLoginDataRepository.UpdateAsync(result);
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}