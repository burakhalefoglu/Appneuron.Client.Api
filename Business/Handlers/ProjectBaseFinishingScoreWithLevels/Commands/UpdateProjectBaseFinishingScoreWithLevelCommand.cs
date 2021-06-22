
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
using Business.Handlers.ProjectBaseFinishingScoreWithLevels.ValidationRules;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.ProjectBaseFinishingScoreWithLevels.Commands
{


    public class UpdateProjectBaseFinishingScoreWithLevelCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public LevelBaseScore[] LevelBaseScore { get; set; }

        public class UpdateProjectBaseFinishingScoreWithLevelCommandHandler : IRequestHandler<UpdateProjectBaseFinishingScoreWithLevelCommand, IResult>
        {
            private readonly IProjectBaseFinishingScoreWithLevelRepository _projectBaseFinishingScoreWithLevelRepository;
            private readonly IMediator _mediator;

            public UpdateProjectBaseFinishingScoreWithLevelCommandHandler(IProjectBaseFinishingScoreWithLevelRepository projectBaseFinishingScoreWithLevelRepository, IMediator mediator)
            {
                _projectBaseFinishingScoreWithLevelRepository = projectBaseFinishingScoreWithLevelRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateProjectBaseFinishingScoreWithLevelValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateProjectBaseFinishingScoreWithLevelCommand request, CancellationToken cancellationToken)
            {



                var projectBaseFinishingScoreWithLevel = new FinishingScoreWithLevel();
                projectBaseFinishingScoreWithLevel.ProjectId = request.ProjectId;
                projectBaseFinishingScoreWithLevel.LevelBaseScore = request.LevelBaseScore;

                await _projectBaseFinishingScoreWithLevelRepository.UpdateAsync(request.Id, projectBaseFinishingScoreWithLevel);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

