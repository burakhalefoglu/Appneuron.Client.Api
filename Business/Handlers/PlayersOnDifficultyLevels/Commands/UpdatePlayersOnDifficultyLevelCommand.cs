
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
using Business.Handlers.PlayersOnDifficultyLevels.ValidationRules;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.PlayersOnDifficultyLevels.Commands
{


    public class UpdatePlayersOnDifficultyLevelCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public PlayerCountOnDifficulty[] PlayerCountOnDifficulty { get; set; }

        public class UpdatePlayersOnDifficultyLevelCommandHandler : IRequestHandler<UpdatePlayersOnDifficultyLevelCommand, IResult>
        {
            private readonly IPlayersOnDifficultyLevelRepository _playersOnDifficultyLevelRepository;
            private readonly IMediator _mediator;

            public UpdatePlayersOnDifficultyLevelCommandHandler(IPlayersOnDifficultyLevelRepository playersOnDifficultyLevelRepository, IMediator mediator)
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



                var playersOnDifficultyLevel = new ProjectBasePlayerCountOnDifficultyLevel();
                playersOnDifficultyLevel.ProjectId = request.ProjectId;
                playersOnDifficultyLevel.PlayerCountOnDifficulty = request.PlayerCountOnDifficulty;


                await _playersOnDifficultyLevelRepository.UpdateAsync(request.Id, playersOnDifficultyLevel);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

