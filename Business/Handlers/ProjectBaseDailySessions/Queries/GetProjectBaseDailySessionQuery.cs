
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;

namespace Business.Handlers.ProjectBaseDailySessions.Queries
{
    public class GetProjectBaseDailySessionQuery : IRequest<IDataResult<ProjectDailySession>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetProjectBaseDailySessionQueryHandler : IRequestHandler<GetProjectBaseDailySessionQuery, IDataResult<ProjectDailySession>>
        {
            private readonly IProjectBaseDailySessionRepository _projectBaseDailySessionRepository;
            private readonly IMediator _mediator;

            public GetProjectBaseDailySessionQueryHandler(IProjectBaseDailySessionRepository projectBaseDailySessionRepository, IMediator mediator)
            {
                _projectBaseDailySessionRepository = projectBaseDailySessionRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ProjectDailySession>> Handle(GetProjectBaseDailySessionQuery request, CancellationToken cancellationToken)
            {
                var projectBaseDailySession = await _projectBaseDailySessionRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<ProjectDailySession>(projectBaseDailySession);
            }
        }
    }
}
