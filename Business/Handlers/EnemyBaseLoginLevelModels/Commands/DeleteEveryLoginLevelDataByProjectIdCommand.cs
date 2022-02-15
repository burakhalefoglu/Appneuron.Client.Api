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

namespace Business.Handlers.EnemyBaseLoginLevelModels.Commands
{
    /// <summary>
    /// </summary>
    public class DeleteEnemyBaseLoginLevelModelByProjectIdCommand : IRequest<IResult>
    {
        public long ProjectId { get; set; }

        public class
            DeleteEnemyBaseLoginLevelModelByProjectIdCommandHandler : IRequestHandler<
                DeleteEnemyBaseLoginLevelModelByProjectIdCommand, IResult>
        {
            private readonly IEnemyBaseLoginLevelModelRepository _everyLoginLevelDataRepository;
            private readonly IMediator _mediator;

            public DeleteEnemyBaseLoginLevelModelByProjectIdCommandHandler(
                IEnemyBaseLoginLevelModelRepository everyLoginLevelDataRepository, IMediator mediator)
            {
                _everyLoginLevelDataRepository = everyLoginLevelDataRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteEnemyBaseLoginLevelModelByProjectIdCommand request,
                CancellationToken cancellationToken)
            {
                var result =
                    await _everyLoginLevelDataRepository.GetAsync(p =>
                        p.ProjectId == request.ProjectId && p.Status == true);
                if (result is null)
                    return new ErrorResult(Messages.NotFound);
                result.Status = false;

                await _everyLoginLevelDataRepository.UpdateAsync(result);
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}