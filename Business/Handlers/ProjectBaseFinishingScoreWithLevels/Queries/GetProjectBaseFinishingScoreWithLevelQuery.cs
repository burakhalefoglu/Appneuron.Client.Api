
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

namespace Business.Handlers.ProjectBaseFinishingScoreWithLevels.Queries
{
    public class GetProjectBaseFinishingScoreWithLevelQuery : IRequest<IDataResult<FinishingScoreWithLevel>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetProjectBaseFinishingScoreWithLevelQueryHandler : IRequestHandler<GetProjectBaseFinishingScoreWithLevelQuery, IDataResult<FinishingScoreWithLevel>>
        {
            private readonly IProjectBaseFinishingScoreWithLevelRepository _projectBaseFinishingScoreWithLevelRepository;
            private readonly IMediator _mediator;

            public GetProjectBaseFinishingScoreWithLevelQueryHandler(IProjectBaseFinishingScoreWithLevelRepository projectBaseFinishingScoreWithLevelRepository, IMediator mediator)
            {
                _projectBaseFinishingScoreWithLevelRepository = projectBaseFinishingScoreWithLevelRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<FinishingScoreWithLevel>> Handle(GetProjectBaseFinishingScoreWithLevelQuery request, CancellationToken cancellationToken)
            {
                var projectBaseFinishingScoreWithLevel = await _projectBaseFinishingScoreWithLevelRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<FinishingScoreWithLevel>(projectBaseFinishingScoreWithLevel);
            }
        }
    }
}
