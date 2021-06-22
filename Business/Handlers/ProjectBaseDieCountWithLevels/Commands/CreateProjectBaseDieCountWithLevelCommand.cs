
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
using Business.Handlers.ProjectBaseDieCountWithLevels.ValidationRules;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.ProjectBaseDieCountWithLevels.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateProjectBaseDieCountWithLevelCommand : IRequest<IResult>
    {

        public string ProjectId { get; set; }
        public LevelBaseDieCount[] LevelBaseDieCount { get; set; }


        public class CreateProjectBaseDieCountWithLevelCommandHandler : IRequestHandler<CreateProjectBaseDieCountWithLevelCommand, IResult>
        {
            private readonly IProjectBaseDieCountWithLevelRepository _projectBaseDieCountWithLevelRepository;
            private readonly IMediator _mediator;
            public CreateProjectBaseDieCountWithLevelCommandHandler(IProjectBaseDieCountWithLevelRepository projectBaseDieCountWithLevelRepository, IMediator mediator)
            {
                _projectBaseDieCountWithLevelRepository = projectBaseDieCountWithLevelRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateProjectBaseDieCountWithLevelValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateProjectBaseDieCountWithLevelCommand request, CancellationToken cancellationToken)
            {
                var isThereProjectBaseDieCountWithLevelRecord = _projectBaseDieCountWithLevelRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isThereProjectBaseDieCountWithLevelRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedProjectBaseDieCountWithLevel = new DieCountWithLevel
                {
                    ProjectId = request.ProjectId,
                    LevelBaseDieCount = request.LevelBaseDieCount
                };

                await _projectBaseDieCountWithLevelRepository.AddAsync(addedProjectBaseDieCountWithLevel);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}