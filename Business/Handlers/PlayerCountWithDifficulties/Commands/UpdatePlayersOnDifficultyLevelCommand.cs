using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.PlayerCountWithDifficulties.ValidationRules;
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

namespace Business.Handlers.PlayerCountWithDifficulties.Commands
{
    public class UpdatePlayersOnDifficultyLevelCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public int DifficultyLevel { get; set; }
        public long PlayerCount { get; set; }

        public class UpdatePlayersOnDifficultyLevelCommandHandler : IRequestHandler<UpdatePlayersOnDifficultyLevelCommand, IResult>
        {
            private readonly IPlayerCountOnDifficultyLevelRepository _playersOnDifficultyLevelRepository;
            private readonly IMediator _mediator;

            public UpdatePlayersOnDifficultyLevelCommandHandler(IPlayerCountOnDifficultyLevelRepository playersOnDifficultyLevelRepository, IMediator mediator)
            {
                _playersOnDifficultyLevelRepository = playersOnDifficultyLevelRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdatePlayersOnDifficultyLevelValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdatePlayersOnDifficultyLevelCommand request, CancellationToken cancellationToken)
            {
                var playersOnDifficultyLevel = new PlayerCountWithDifficulty();
                playersOnDifficultyLevel.ProjectId = request.ProjectId;
                playersOnDifficultyLevel.PlayerCount = request.PlayerCount;
                playersOnDifficultyLevel.DifficultyLevel = request.DifficultyLevel;

                await _playersOnDifficultyLevelRepository.UpdateAsync(request.Id, playersOnDifficultyLevel);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}