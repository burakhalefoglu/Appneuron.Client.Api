using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.LevelBaseDieCountWithDifficulties.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.ChartModels;
using MediatR;
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.LevelBaseDieCountWithDifficulties.Commands
{
    public class UpdateLevelBaseDieCountWithDifficultyCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public int LevelIndex { get; set; }
        public long DieCount { get; set; }
        public System.DateTime DateTime { get; set; }
        public int DifficultyLevel { get; set; }

        public class UpdateLevelBaseDieCountWithDifficultyCommandHandler : IRequestHandler<UpdateLevelBaseDieCountWithDifficultyCommand, IResult>
        {
            private readonly ILevelBaseDieCountWithDifficultyRepository _levelBaseDieCountWithDifficultyRepository;
            private readonly IMediator _mediator;

            public UpdateLevelBaseDieCountWithDifficultyCommandHandler(ILevelBaseDieCountWithDifficultyRepository levelBaseDieCountWithDifficultyRepository, IMediator mediator)
            {
                _levelBaseDieCountWithDifficultyRepository = levelBaseDieCountWithDifficultyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateLevelBaseDieCountWithDifficultyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateLevelBaseDieCountWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                var levelBaseDieCountWithDifficulty = new LevelBaseDieCountWithDifficulty();
                levelBaseDieCountWithDifficulty.ProjectId = request.ProjectId;
                levelBaseDieCountWithDifficulty.LevelIndex = request.LevelIndex;
                levelBaseDieCountWithDifficulty.DieCount = request.DieCount;
                levelBaseDieCountWithDifficulty.DateTime = request.DateTime;
                levelBaseDieCountWithDifficulty.DifficultyLevel = request.DifficultyLevel;

                await _levelBaseDieCountWithDifficultyRepository.UpdateAsync(request.Id, levelBaseDieCountWithDifficulty);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}