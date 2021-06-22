
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

namespace Business.Handlers.ProjectBaseBuyingCountWithDifficulties.Queries
{
    public class GetProjectBaseBuyingCountWithDifficultyQuery : IRequest<IDataResult<ProjectBuyingCountWithDifficulty>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetProjectBaseBuyingCountWithDifficultyQueryHandler : IRequestHandler<GetProjectBaseBuyingCountWithDifficultyQuery, IDataResult<ProjectBuyingCountWithDifficulty>>
        {
            private readonly IProjectBaseBuyingCountWithDifficultyRepository _projectBaseBuyingCountWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetProjectBaseBuyingCountWithDifficultyQueryHandler(IProjectBaseBuyingCountWithDifficultyRepository projectBaseBuyingCountWithDifficultyRepository, IMediator mediator)
            {
                _projectBaseBuyingCountWithDifficultyRepository = projectBaseBuyingCountWithDifficultyRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ProjectBuyingCountWithDifficulty>> Handle(GetProjectBaseBuyingCountWithDifficultyQuery request, CancellationToken cancellationToken)
            {
                var projectBaseBuyingCountWithDifficulty = await _projectBaseBuyingCountWithDifficultyRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<ProjectBuyingCountWithDifficulty>(projectBaseBuyingCountWithDifficulty);
            }
        }
    }
}
