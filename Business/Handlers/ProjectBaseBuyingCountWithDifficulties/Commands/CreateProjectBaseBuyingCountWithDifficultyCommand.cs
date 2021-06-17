
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
using Business.Handlers.ProjectBaseBuyingCountWithDifficulties.ValidationRules;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.ProjectBaseBuyingCountWithDifficulties.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateProjectBaseBuyingCountWithDifficultyCommand : IRequest<IResult>
    {

        public string ProjectId { get; set; }
        public BuyingCountWithDifficulty[] BuyingCountWithDifficulty { get; set; }


        public class CreateProjectBaseBuyingCountWithDifficultyCommandHandler : IRequestHandler<CreateProjectBaseBuyingCountWithDifficultyCommand, IResult>
        {
            private readonly IProjectBaseBuyingCountWithDifficultyRepository _projectBaseBuyingCountWithDifficultyRepository;
            private readonly IMediator _mediator;
            public CreateProjectBaseBuyingCountWithDifficultyCommandHandler(IProjectBaseBuyingCountWithDifficultyRepository projectBaseBuyingCountWithDifficultyRepository, IMediator mediator)
            {
                _projectBaseBuyingCountWithDifficultyRepository = projectBaseBuyingCountWithDifficultyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateProjectBaseBuyingCountWithDifficultyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateProjectBaseBuyingCountWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                var isThereProjectBaseBuyingCountWithDifficultyRecord = _projectBaseBuyingCountWithDifficultyRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isThereProjectBaseBuyingCountWithDifficultyRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedProjectBaseBuyingCountWithDifficulty = new ProjectBaseBuyingCountWithDifficulty
                {
                    ProjectId = request.ProjectId,
                    BuyingCountWithDifficulty = request.BuyingCountWithDifficulty

                };

                await _projectBaseBuyingCountWithDifficultyRepository.AddAsync(addedProjectBaseBuyingCountWithDifficulty);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}