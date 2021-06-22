
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

namespace Business.Handlers.ProjectBaseDailySessions.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteProjectBaseDailySessionCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeleteProjectBaseDailySessionCommandHandler : IRequestHandler<DeleteProjectBaseDailySessionCommand, IResult>
        {
            private readonly IProjectBaseDailySessionRepository _projectBaseDailySessionRepository;
            private readonly IMediator _mediator;

            public DeleteProjectBaseDailySessionCommandHandler(IProjectBaseDailySessionRepository projectBaseDailySessionRepository, IMediator mediator)
            {
                _projectBaseDailySessionRepository = projectBaseDailySessionRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteProjectBaseDailySessionCommand request, CancellationToken cancellationToken)
            {


                await _projectBaseDailySessionRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

