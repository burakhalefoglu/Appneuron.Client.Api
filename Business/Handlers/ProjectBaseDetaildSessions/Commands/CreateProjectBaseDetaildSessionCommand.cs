
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
using Business.Handlers.ProjectBaseDetaildSessions.ValidationRules;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.ProjectBaseDetaildSessions.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateProjectBaseDetaildSessionCommand : IRequest<IResult>
    {

        public string ProjectId { get; set; }
        public AvarageSessionByLevel[] AvarageSessionByLevel { get; set; }


        public class CreateProjectBaseDetaildSessionCommandHandler : IRequestHandler<CreateProjectBaseDetaildSessionCommand, IResult>
        {
            private readonly IProjectBaseDetaildSessionRepository _projectBaseDetaildSessionRepository;
            private readonly IMediator _mediator;
            public CreateProjectBaseDetaildSessionCommandHandler(IProjectBaseDetaildSessionRepository projectBaseDetaildSessionRepository, IMediator mediator)
            {
                _projectBaseDetaildSessionRepository = projectBaseDetaildSessionRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateProjectBaseDetaildSessionValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateProjectBaseDetaildSessionCommand request, CancellationToken cancellationToken)
            {
                var isThereProjectBaseDetaildSessionRecord = _projectBaseDetaildSessionRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isThereProjectBaseDetaildSessionRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedProjectBaseDetaildSession = new DetaildSession
                {
                    ProjectId = request.ProjectId,
                    AvarageSessionByLevel = request.AvarageSessionByLevel

                };

                await _projectBaseDetaildSessionRepository.AddAsync(addedProjectBaseDetaildSession);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}