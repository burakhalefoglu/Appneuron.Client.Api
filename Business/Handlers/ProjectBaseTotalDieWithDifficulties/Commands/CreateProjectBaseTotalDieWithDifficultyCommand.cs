
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
using Business.Handlers.ProjectBaseTotalDieWithDifficulties.ValidationRules;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.ProjectBaseTotalDieWithDifficulties.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateProjectBaseTotalDieWithDifficultyCommand : IRequest<IResult>
    {

        public string ProjectId { get; set; }
        public TotalDieWithDifficulty[] TotalDieWithDifficulty { get; set; }


        public class CreateProjectBaseTotalDieWithDifficultyCommandHandler : IRequestHandler<CreateProjectBaseTotalDieWithDifficultyCommand, IResult>
        {
            private readonly IProjectBaseTotalDieWithDifficultyRepository _projectBaseTotalDieWithDifficultyRepository;
            private readonly IMediator _mediator;
            public CreateProjectBaseTotalDieWithDifficultyCommandHandler(IProjectBaseTotalDieWithDifficultyRepository projectBaseTotalDieWithDifficultyRepository, IMediator mediator)
            {
                _projectBaseTotalDieWithDifficultyRepository = projectBaseTotalDieWithDifficultyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateProjectBaseTotalDieWithDifficultyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateProjectBaseTotalDieWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                var isThereProjectBaseTotalDieWithDifficultyRecord = _projectBaseTotalDieWithDifficultyRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isThereProjectBaseTotalDieWithDifficultyRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedProjectBaseTotalDieWithDifficulty = new ProjectBaseTotalDieWithDifficulty
                {
                    ProjectId = request.ProjectId,
                    TotalDieWithDifficulty = request.TotalDieWithDifficulty

                };

                await _projectBaseTotalDieWithDifficultyRepository.AddAsync(addedProjectBaseTotalDieWithDifficulty);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}