
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.ProjectBasePowerUsageByDifficulties.ValidationRules;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.ProjectBasePowerUsageByDifficulties.Commands
{


    public class UpdateProjectBasePowerUsageByDifficultyCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public PowerUsageWithDifficulty[] PowerUsageWithDifficultyLevel { get; set; }

        public class UpdateProjectBasePowerUsageByDifficultyCommandHandler : IRequestHandler<UpdateProjectBasePowerUsageByDifficultyCommand, IResult>
        {
            private readonly IProjectBasePowerUsageByDifficultyRepository _projectBasePowerUsageByDifficultyRepository;
            private readonly IMediator _mediator;

            public UpdateProjectBasePowerUsageByDifficultyCommandHandler(IProjectBasePowerUsageByDifficultyRepository projectBasePowerUsageByDifficultyRepository, IMediator mediator)
            {
                _projectBasePowerUsageByDifficultyRepository = projectBasePowerUsageByDifficultyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateProjectBasePowerUsageByDifficultyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateProjectBasePowerUsageByDifficultyCommand request, CancellationToken cancellationToken)
            {



                var projectBasePowerUsageByDifficulty = new ProjectBasePowerUsageByDifficulty();
                projectBasePowerUsageByDifficulty.ProjectId = request.ProjectId;
                projectBasePowerUsageByDifficulty.PowerUsageWithDifficultyLevel = request.PowerUsageWithDifficultyLevel;


                await _projectBasePowerUsageByDifficultyRepository.UpdateAsync(request.Id, projectBasePowerUsageByDifficulty);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

