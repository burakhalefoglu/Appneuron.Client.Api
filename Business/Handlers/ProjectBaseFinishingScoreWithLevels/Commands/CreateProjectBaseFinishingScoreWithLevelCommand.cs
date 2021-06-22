
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.ProjectBaseFinishingScoreWithLevels.ValidationRules;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.ProjectBaseFinishingScoreWithLevels.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateProjectBaseFinishingScoreWithLevelCommand : IRequest<IResult>
    {

        public string ProjectId { get; set; }
        public LevelBaseScore[] LevelBaseScore { get; set; }


        public class CreateProjectBaseFinishingScoreWithLevelCommandHandler : IRequestHandler<CreateProjectBaseFinishingScoreWithLevelCommand, IResult>
        {
            private readonly IProjectBaseFinishingScoreWithLevelRepository _projectBaseFinishingScoreWithLevelRepository;
            private readonly IMediator _mediator;
            public CreateProjectBaseFinishingScoreWithLevelCommandHandler(IProjectBaseFinishingScoreWithLevelRepository projectBaseFinishingScoreWithLevelRepository, IMediator mediator)
            {
                _projectBaseFinishingScoreWithLevelRepository = projectBaseFinishingScoreWithLevelRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateProjectBaseFinishingScoreWithLevelValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateProjectBaseFinishingScoreWithLevelCommand request, CancellationToken cancellationToken)
            {
                var isThereProjectBaseFinishingScoreWithLevelRecord = _projectBaseFinishingScoreWithLevelRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isThereProjectBaseFinishingScoreWithLevelRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedProjectBaseFinishingScoreWithLevel = new FinishingScoreWithLevel
                {
                    ProjectId = request.ProjectId,
                    LevelBaseScore = request.LevelBaseScore
                };

                await _projectBaseFinishingScoreWithLevelRepository.AddAsync(addedProjectBaseFinishingScoreWithLevel);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}