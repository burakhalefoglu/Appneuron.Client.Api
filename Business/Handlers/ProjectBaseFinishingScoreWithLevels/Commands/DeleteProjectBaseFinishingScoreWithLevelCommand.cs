
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Business.Handlers.ProjectBaseFinishingScoreWithLevels.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteProjectBaseFinishingScoreWithLevelCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeleteProjectBaseFinishingScoreWithLevelCommandHandler : IRequestHandler<DeleteProjectBaseFinishingScoreWithLevelCommand, IResult>
        {
            private readonly IProjectBaseFinishingScoreWithLevelRepository _projectBaseFinishingScoreWithLevelRepository;
            private readonly IMediator _mediator;

            public DeleteProjectBaseFinishingScoreWithLevelCommandHandler(IProjectBaseFinishingScoreWithLevelRepository projectBaseFinishingScoreWithLevelRepository, IMediator mediator)
            {
                _projectBaseFinishingScoreWithLevelRepository = projectBaseFinishingScoreWithLevelRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteProjectBaseFinishingScoreWithLevelCommand request, CancellationToken cancellationToken)
            {


                await _projectBaseFinishingScoreWithLevelRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

