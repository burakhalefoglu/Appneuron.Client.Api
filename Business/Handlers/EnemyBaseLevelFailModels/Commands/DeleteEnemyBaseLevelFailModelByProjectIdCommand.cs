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

namespace Business.Handlers.EnemyBaseLevelFailModels.Commands
{
    /// <summary>
    /// </summary>
    public class DeleteEnemyBaseLevelFailModelByProjectIdCommand : IRequest<IResult>
    {
        public long ProjectId { get; set; }

        public class
            DeleteEnemyBaseLevelFailModelByProjectIdCommandHandler : IRequestHandler<DeleteEnemyBaseLevelFailModelByProjectIdCommand,
                IResult>
        {
            private readonly IEnemyBaseLevelFailModelRepository _levelBaseDieDataRepository;
            private readonly IMediator _mediator;

            public DeleteEnemyBaseLevelFailModelByProjectIdCommandHandler(
                IEnemyBaseLevelFailModelRepository levelBaseDieDataRepository, IMediator mediator)
            {
                _levelBaseDieDataRepository = levelBaseDieDataRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteEnemyBaseLevelFailModelByProjectIdCommand request,
                CancellationToken cancellationToken)
            {
                var result =
                    await _levelBaseDieDataRepository.GetAsync(
                        p => p.ProjectId == request.ProjectId && p.Status == true);
                if (result is null)
                    return new ErrorResult(Messages.NotFound);
                result.Status = false;

                await _levelBaseDieDataRepository.UpdateAsync(result);
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}