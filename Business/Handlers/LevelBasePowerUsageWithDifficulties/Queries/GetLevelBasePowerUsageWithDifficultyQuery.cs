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

namespace Business.Handlers.LevelBasePowerUsageWithDifficulties.Queries
{
    public class GetLevelBasePowerUsageWithDifficultyQuery : IRequest<IDataResult<LevelBasePowerUsageWithDifficulty>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetLevelBasePowerUsageWithDifficultyQueryHandler : IRequestHandler<GetLevelBasePowerUsageWithDifficultyQuery, IDataResult<LevelBasePowerUsageWithDifficulty>>
        {
            private readonly ILevelBasePowerUsageWithDifficultyRepository _levelBasePowerUsageWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetLevelBasePowerUsageWithDifficultyQueryHandler(ILevelBasePowerUsageWithDifficultyRepository levelBasePowerUsageWithDifficultyRepository, IMediator mediator)
            {
                _levelBasePowerUsageWithDifficultyRepository = levelBasePowerUsageWithDifficultyRepository;
                _mediator = mediator;
            }

            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<LevelBasePowerUsageWithDifficulty>> Handle(GetLevelBasePowerUsageWithDifficultyQuery request, CancellationToken cancellationToken)
            {
                var levelBasePowerUsageWithDifficulty = await _levelBasePowerUsageWithDifficultyRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<LevelBasePowerUsageWithDifficulty>(levelBasePowerUsageWithDifficulty);
            }
        }
    }
}