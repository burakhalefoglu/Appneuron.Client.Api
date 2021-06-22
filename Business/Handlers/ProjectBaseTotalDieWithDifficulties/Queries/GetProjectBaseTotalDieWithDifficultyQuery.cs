
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

namespace Business.Handlers.ProjectBaseTotalDieWithDifficulties.Queries
{
    public class GetProjectBaseTotalDieWithDifficultyQuery : IRequest<IDataResult<ProjectTotalDieWithDifficulty>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetProjectBaseTotalDieWithDifficultyQueryHandler : IRequestHandler<GetProjectBaseTotalDieWithDifficultyQuery, IDataResult<ProjectTotalDieWithDifficulty>>
        {
            private readonly IProjectBaseTotalDieWithDifficultyRepository _projectBaseTotalDieWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetProjectBaseTotalDieWithDifficultyQueryHandler(IProjectBaseTotalDieWithDifficultyRepository projectBaseTotalDieWithDifficultyRepository, IMediator mediator)
            {
                _projectBaseTotalDieWithDifficultyRepository = projectBaseTotalDieWithDifficultyRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ProjectTotalDieWithDifficulty>> Handle(GetProjectBaseTotalDieWithDifficultyQuery request, CancellationToken cancellationToken)
            {
                var projectBaseTotalDieWithDifficulty = await _projectBaseTotalDieWithDifficultyRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<ProjectTotalDieWithDifficulty>(projectBaseTotalDieWithDifficulty);
            }
        }
    }
}
