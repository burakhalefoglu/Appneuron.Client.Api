
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
using Business.Handlers.ProjectBaseDailySessions.ValidationRules;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.ProjectBaseDailySessions.Commands
{


    public class UpdateProjectBaseDailySessionCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public DailySession[] DailySession { get; set; }

        public class UpdateProjectBaseDailySessionCommandHandler : IRequestHandler<UpdateProjectBaseDailySessionCommand, IResult>
        {
            private readonly IProjectBaseDailySessionRepository _projectBaseDailySessionRepository;
            private readonly IMediator _mediator;

            public UpdateProjectBaseDailySessionCommandHandler(IProjectBaseDailySessionRepository projectBaseDailySessionRepository, IMediator mediator)
            {
                _projectBaseDailySessionRepository = projectBaseDailySessionRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateProjectBaseDailySessionValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateProjectBaseDailySessionCommand request, CancellationToken cancellationToken)
            {



                var projectBaseDailySession = new ProjectDailySession();
                projectBaseDailySession.ProjectId = request.ProjectId;
                projectBaseDailySession.DailySession = request.DailySession;

                await _projectBaseDailySessionRepository.UpdateAsync(request.Id, projectBaseDailySession);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

