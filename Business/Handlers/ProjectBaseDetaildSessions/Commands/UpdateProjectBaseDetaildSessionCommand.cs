
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
using Business.Handlers.ProjectBaseDetaildSessions.ValidationRules;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.ProjectBaseDetaildSessions.Commands
{


    public class UpdateProjectBaseDetaildSessionCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public AvarageSessionByLevel[] AvarageSessionByLevel { get; set; }

        public class UpdateProjectBaseDetaildSessionCommandHandler : IRequestHandler<UpdateProjectBaseDetaildSessionCommand, IResult>
        {
            private readonly IProjectBaseDetaildSessionRepository _projectBaseDetaildSessionRepository;
            private readonly IMediator _mediator;

            public UpdateProjectBaseDetaildSessionCommandHandler(IProjectBaseDetaildSessionRepository projectBaseDetaildSessionRepository, IMediator mediator)
            {
                _projectBaseDetaildSessionRepository = projectBaseDetaildSessionRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateProjectBaseDetaildSessionValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateProjectBaseDetaildSessionCommand request, CancellationToken cancellationToken)
            {



                var projectBaseDetaildSession = new DetaildSession();
                projectBaseDetaildSession.ProjectId = request.ProjectId;
                projectBaseDetaildSession.AvarageSessionByLevel = request.AvarageSessionByLevel;


                await _projectBaseDetaildSessionRepository.UpdateAsync(request.Id, projectBaseDetaildSession);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

