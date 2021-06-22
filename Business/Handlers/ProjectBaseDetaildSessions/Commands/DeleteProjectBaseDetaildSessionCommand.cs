
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

namespace Business.Handlers.ProjectBaseDetaildSessions.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteProjectBaseDetaildSessionCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeleteProjectBaseDetaildSessionCommandHandler : IRequestHandler<DeleteProjectBaseDetaildSessionCommand, IResult>
        {
            private readonly IProjectBaseDetaildSessionRepository _projectBaseDetaildSessionRepository;
            private readonly IMediator _mediator;

            public DeleteProjectBaseDetaildSessionCommandHandler(IProjectBaseDetaildSessionRepository projectBaseDetaildSessionRepository, IMediator mediator)
            {
                _projectBaseDetaildSessionRepository = projectBaseDetaildSessionRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteProjectBaseDetaildSessionCommand request, CancellationToken cancellationToken)
            {


                await _projectBaseDetaildSessionRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

