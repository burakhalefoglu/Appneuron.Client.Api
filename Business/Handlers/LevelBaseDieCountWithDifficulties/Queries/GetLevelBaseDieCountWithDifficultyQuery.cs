using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.ChartModels;
using MediatR;
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.LevelBaseDieCountWithDifficulties.Queries
{
    public class GetLevelBaseDieCountWithDifficultyQuery : IRequest<IDataResult<LevelBaseDieCountWithDifficulty>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetLevelBaseDieCountWithDifficultyQueryHandler : IRequestHandler<GetLevelBaseDieCountWithDifficultyQuery, IDataResult<LevelBaseDieCountWithDifficulty>>
        {
            private readonly ILevelBaseDieCountWithDifficultyRepository _levelBaseDieCountWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetLevelBaseDieCountWithDifficultyQueryHandler(ILevelBaseDieCountWithDifficultyRepository levelBaseDieCountWithDifficultyRepository, IMediator mediator)
            {
                _levelBaseDieCountWithDifficultyRepository = levelBaseDieCountWithDifficultyRepository;
                _mediator = mediator;
            }

            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<LevelBaseDieCountWithDifficulty>> Handle(GetLevelBaseDieCountWithDifficultyQuery request, CancellationToken cancellationToken)
            {
                var levelBaseDieCountWithDifficulty = await _levelBaseDieCountWithDifficultyRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<LevelBaseDieCountWithDifficulty>(levelBaseDieCountWithDifficulty);
            }
        }
    }
}