
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

namespace Business.Handlers.ProjectBaseDetaildSessions.Queries
{
    public class GetProjectBaseDetaildSessionQuery : IRequest<IDataResult<DetaildSession>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetProjectBaseDetaildSessionQueryHandler : IRequestHandler<GetProjectBaseDetaildSessionQuery, IDataResult<DetaildSession>>
        {
            private readonly IProjectBaseDetaildSessionRepository _projectBaseDetaildSessionRepository;
            private readonly IMediator _mediator;

            public GetProjectBaseDetaildSessionQueryHandler(IProjectBaseDetaildSessionRepository projectBaseDetaildSessionRepository, IMediator mediator)
            {
                _projectBaseDetaildSessionRepository = projectBaseDetaildSessionRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<DetaildSession>> Handle(GetProjectBaseDetaildSessionQuery request, CancellationToken cancellationToken)
            {
                var projectBaseDetaildSession = await _projectBaseDetaildSessionRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<DetaildSession>(projectBaseDetaildSession);
            }
        }
    }
}
