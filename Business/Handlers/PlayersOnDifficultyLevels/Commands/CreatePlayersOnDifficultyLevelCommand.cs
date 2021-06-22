
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.PlayerCountOnDifficultyLevels.ValidationRules;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.PlayerCountOnDifficultyLevels.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreatePlayersOnDifficultyLevelCommand : IRequest<IResult>
    {

        public string ProjectId { get; set; }
        public PlayerCountOnDifficulty[] PlayerCountOnDifficulty { get; set; }

        public class CreatePlayersOnDifficultyLevelCommandHandler : IRequestHandler<CreatePlayersOnDifficultyLevelCommand, IResult>
        {
            private readonly IPlayerCountOnDifficultyLevelRepository _playersOnDifficultyLevelRepository;
            private readonly IMediator _mediator;
            public CreatePlayersOnDifficultyLevelCommandHandler(IPlayerCountOnDifficultyLevelRepository playersOnDifficultyLevelRepository, IMediator mediator)
            {
                _playersOnDifficultyLevelRepository = playersOnDifficultyLevelRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreatePlayersOnDifficultyLevelValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreatePlayersOnDifficultyLevelCommand request, CancellationToken cancellationToken)
            {
                var isTherePlayersOnDifficultyLevelRecord = _playersOnDifficultyLevelRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isTherePlayersOnDifficultyLevelRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedPlayersOnDifficultyLevel = new PlayerCountOnDifficultyLevel
                {
                    ProjectId = request.ProjectId,
                    PlayerCountOnDifficulty = request.PlayerCountOnDifficulty

                };

                await _playersOnDifficultyLevelRepository.AddAsync(addedPlayersOnDifficultyLevel);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}