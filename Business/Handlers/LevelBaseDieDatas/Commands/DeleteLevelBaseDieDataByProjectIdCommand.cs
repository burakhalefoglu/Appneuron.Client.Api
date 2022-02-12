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

namespace Business.Handlers.LevelBaseDieDatas.Commands
{
    /// <summary>
    /// </summary>
    public class DeleteLevelBaseDieDataByProjectIdCommand : IRequest<IResult>
    {
        public long ProjectId { get; set; }

        public class
            DeleteLevelBaseDieDataByProjectIdCommandHandler : IRequestHandler<DeleteLevelBaseDieDataByProjectIdCommand,
                IResult>
        {
            private readonly ILevelBaseDieDataRepository _levelBaseDieDataRepository;
            private readonly IMediator _mediator;

            public DeleteLevelBaseDieDataByProjectIdCommandHandler(
                ILevelBaseDieDataRepository levelBaseDieDataRepository, IMediator mediator)
            {
                _levelBaseDieDataRepository = levelBaseDieDataRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteLevelBaseDieDataByProjectIdCommand request,
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