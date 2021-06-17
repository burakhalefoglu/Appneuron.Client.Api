
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
using Business.Handlers.ProjectBaseAdvClicks.ValidationRules;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.ProjectBaseAdvClicks.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateProjectBaseAdvClickCommand : IRequest<IResult>
    {

        public string ProjectId { get; set; }
        public AdvClickWithDifficulty[] AdvClickWithDifficulty { get; set; }


        public class CreateProjectBaseAdvClickCommandHandler : IRequestHandler<CreateProjectBaseAdvClickCommand, IResult>
        {
            private readonly IProjectBaseAdvClickRepository _projectBaseAdvClickRepository;
            private readonly IMediator _mediator;
            public CreateProjectBaseAdvClickCommandHandler(IProjectBaseAdvClickRepository projectBaseAdvClickRepository, IMediator mediator)
            {
                _projectBaseAdvClickRepository = projectBaseAdvClickRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateProjectBaseAdvClickValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateProjectBaseAdvClickCommand request, CancellationToken cancellationToken)
            {
                var isThereProjectBaseAdvClickRecord = _projectBaseAdvClickRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isThereProjectBaseAdvClickRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedProjectBaseAdvClick = new ProjectBaseAdvClick
                {
                    ProjectId = request.ProjectId,
                    AdvClickWithDifficulty = request.AdvClickWithDifficulty

                };

                await _projectBaseAdvClickRepository.AddAsync(addedProjectBaseAdvClick);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}