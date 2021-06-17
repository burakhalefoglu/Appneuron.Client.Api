
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
using Business.Handlers.PlayersOnLevels.ValidationRules;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.PlayersOnLevels.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreatePlayersOnLevelCommand : IRequest<IResult>
    {

        public string ProjectID { get; set; }
        public PlayerCountOnLevel[] PlayerCountOnLevel { get; set; }

        public class CreatePlayersOnLevelCommandHandler : IRequestHandler<CreatePlayersOnLevelCommand, IResult>
        {
            private readonly IPlayersOnLevelRepository _playersOnLevelRepository;
            private readonly IMediator _mediator;
            public CreatePlayersOnLevelCommandHandler(IPlayersOnLevelRepository playersOnLevelRepository, IMediator mediator)
            {
                _playersOnLevelRepository = playersOnLevelRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreatePlayersOnLevelValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreatePlayersOnLevelCommand request, CancellationToken cancellationToken)
            {
                var isTherePlayersOnLevelRecord = _playersOnLevelRepository.Any(u => u.ProjectID == request.ProjectID);

                if (isTherePlayersOnLevelRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedPlayersOnLevel = new ProjectBasePlayerCountsOnLevel
                {
                    ProjectID = request.ProjectID,
                    PlayerCountOnLevel = request.PlayerCountOnLevel

                };

                await _playersOnLevelRepository.AddAsync(addedPlayersOnLevel);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}