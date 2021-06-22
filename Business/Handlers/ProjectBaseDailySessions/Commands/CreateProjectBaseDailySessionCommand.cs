
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
using Business.Handlers.ProjectBaseDailySessions.ValidationRules;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.ProjectBaseDailySessions.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateProjectBaseDailySessionCommand : IRequest<IResult>
    {

        public string ProjectId { get; set; }
        public Entities.Concrete.ChartModels.OneToOne.DailySession[] DailySession { get; set; }


        public class CreateProjectBaseDailySessionCommandHandler : IRequestHandler<CreateProjectBaseDailySessionCommand, IResult>
        {
            private readonly IProjectBaseDailySessionRepository _projectBaseDailySessionRepository;
            private readonly IMediator _mediator;
            public CreateProjectBaseDailySessionCommandHandler(IProjectBaseDailySessionRepository projectBaseDailySessionRepository, IMediator mediator)
            {
                _projectBaseDailySessionRepository = projectBaseDailySessionRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateProjectBaseDailySessionValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateProjectBaseDailySessionCommand request, CancellationToken cancellationToken)
            {
                var isThereProjectBaseDailySessionRecord = _projectBaseDailySessionRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isThereProjectBaseDailySessionRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedProjectBaseDailySession = new Entities.Concrete.ChartModels.ProjectDailySession
                {
                    ProjectId = request.ProjectId,
                    DailySession = request.DailySession
                };

                await _projectBaseDailySessionRepository.AddAsync(addedProjectBaseDailySession);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}