
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
using Business.Handlers.PowerUsageByDifficulties.ValidationRules;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.PowerUsageByDifficulties.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateProjectBasePowerUsageByDifficultyCommand : IRequest<IResult>
    {

        public string ProjectId { get; set; }
        public PowerUsageWithDifficulty[] PowerUsageWithDifficultyLevel { get; set; }

        public class CreateProjectBasePowerUsageByDifficultyCommandHandler : IRequestHandler<CreateProjectBasePowerUsageByDifficultyCommand, IResult>
        {
            private readonly IProjectBasePowerUsageByDifficultyRepository _projectBasePowerUsageByDifficultyRepository;
            private readonly IMediator _mediator;
            public CreateProjectBasePowerUsageByDifficultyCommandHandler(IProjectBasePowerUsageByDifficultyRepository projectBasePowerUsageByDifficultyRepository, IMediator mediator)
            {
                _projectBasePowerUsageByDifficultyRepository = projectBasePowerUsageByDifficultyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateProjectBasePowerUsageByDifficultyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateProjectBasePowerUsageByDifficultyCommand request, CancellationToken cancellationToken)
            {
                var isThereProjectBasePowerUsageByDifficultyRecord = _projectBasePowerUsageByDifficultyRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isThereProjectBasePowerUsageByDifficultyRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedProjectBasePowerUsageByDifficulty = new PowerUsageByDifficulty
                {
                    ProjectId = request.ProjectId,
                    PowerUsageWithDifficultyLevel = request.PowerUsageWithDifficultyLevel

                };

                await _projectBasePowerUsageByDifficultyRepository.AddAsync(addedProjectBasePowerUsageByDifficulty);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}