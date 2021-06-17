
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

namespace Business.Handlers.ProjectBaseBuyingCountWithDifficulties.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteProjectBaseBuyingCountWithDifficultyCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeleteProjectBaseBuyingCountWithDifficultyCommandHandler : IRequestHandler<DeleteProjectBaseBuyingCountWithDifficultyCommand, IResult>
        {
            private readonly IProjectBaseBuyingCountWithDifficultyRepository _projectBaseBuyingCountWithDifficultyRepository;
            private readonly IMediator _mediator;

            public DeleteProjectBaseBuyingCountWithDifficultyCommandHandler(IProjectBaseBuyingCountWithDifficultyRepository projectBaseBuyingCountWithDifficultyRepository, IMediator mediator)
            {
                _projectBaseBuyingCountWithDifficultyRepository = projectBaseBuyingCountWithDifficultyRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteProjectBaseBuyingCountWithDifficultyCommand request, CancellationToken cancellationToken)
            {


                await _projectBaseBuyingCountWithDifficultyRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

