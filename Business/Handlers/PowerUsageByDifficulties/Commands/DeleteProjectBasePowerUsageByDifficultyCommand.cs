
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

namespace Business.Handlers.PowerUsageByDifficulties.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteProjectBasePowerUsageByDifficultyCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeleteProjectBasePowerUsageByDifficultyCommandHandler : IRequestHandler<DeleteProjectBasePowerUsageByDifficultyCommand, IResult>
        {
            private readonly IProjectBasePowerUsageByDifficultyRepository _projectBasePowerUsageByDifficultyRepository;
            private readonly IMediator _mediator;

            public DeleteProjectBasePowerUsageByDifficultyCommandHandler(IProjectBasePowerUsageByDifficultyRepository projectBasePowerUsageByDifficultyRepository, IMediator mediator)
            {
                _projectBasePowerUsageByDifficultyRepository = projectBasePowerUsageByDifficultyRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteProjectBasePowerUsageByDifficultyCommand request, CancellationToken cancellationToken)
            {


                await _projectBasePowerUsageByDifficultyRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

