
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

namespace Business.Handlers.ProjectBaseTotalDieWithDifficulties.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteProjectBaseTotalDieWithDifficultyCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeleteProjectBaseTotalDieWithDifficultyCommandHandler : IRequestHandler<DeleteProjectBaseTotalDieWithDifficultyCommand, IResult>
        {
            private readonly IProjectBaseTotalDieWithDifficultyRepository _projectBaseTotalDieWithDifficultyRepository;
            private readonly IMediator _mediator;

            public DeleteProjectBaseTotalDieWithDifficultyCommandHandler(IProjectBaseTotalDieWithDifficultyRepository projectBaseTotalDieWithDifficultyRepository, IMediator mediator)
            {
                _projectBaseTotalDieWithDifficultyRepository = projectBaseTotalDieWithDifficultyRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteProjectBaseTotalDieWithDifficultyCommand request, CancellationToken cancellationToken)
            {


                await _projectBaseTotalDieWithDifficultyRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

