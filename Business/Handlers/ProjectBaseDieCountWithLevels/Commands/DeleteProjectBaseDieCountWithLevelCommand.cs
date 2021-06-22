
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

namespace Business.Handlers.ProjectBaseDieCountWithLevels.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteProjectBaseDieCountWithLevelCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeleteProjectBaseDieCountWithLevelCommandHandler : IRequestHandler<DeleteProjectBaseDieCountWithLevelCommand, IResult>
        {
            private readonly IProjectBaseDieCountWithLevelRepository _projectBaseDieCountWithLevelRepository;
            private readonly IMediator _mediator;

            public DeleteProjectBaseDieCountWithLevelCommandHandler(IProjectBaseDieCountWithLevelRepository projectBaseDieCountWithLevelRepository, IMediator mediator)
            {
                _projectBaseDieCountWithLevelRepository = projectBaseDieCountWithLevelRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteProjectBaseDieCountWithLevelCommand request, CancellationToken cancellationToken)
            {


                await _projectBaseDieCountWithLevelRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

