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

namespace Business.Handlers.LevelBaseSessionModels.Commands
{
    /// <summary>
    /// </summary>
    public class DeleteLevelBaseSessionModelByProjectIdCommand : IRequest<IResult>
    {
        public long ProjectId { get; set; }

        public class
            DeleteLevelBaseSessionModelByProjectIdCommandHandler : IRequestHandler<
                DeleteLevelBaseSessionModelByProjectIdCommand, IResult>
        {
            private readonly ILevelBaseSessionModelRepository _levelBaseSessionDataRepository;
            private readonly IMediator _mediator;

            public DeleteLevelBaseSessionModelByProjectIdCommandHandler(
                ILevelBaseSessionModelRepository levelBaseSessionDataRepository, IMediator mediator)
            {
                _levelBaseSessionDataRepository = levelBaseSessionDataRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteLevelBaseSessionModelByProjectIdCommand request,
                CancellationToken cancellationToken)
            {
                var result =
                    await _levelBaseSessionDataRepository.GetAsync(p =>
                        p.ProjectId == request.ProjectId && p.Status == true);
                if (result is null)
                    return new ErrorResult(Messages.NotFound);
                result.Status = false;

                await _levelBaseSessionDataRepository.UpdateAsync(result);
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}