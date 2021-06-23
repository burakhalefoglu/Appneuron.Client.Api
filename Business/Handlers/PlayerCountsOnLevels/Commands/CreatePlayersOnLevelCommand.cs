using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.PlayerCountsOnLevels.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.ChartModels;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PlayerCountsOnLevels.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class CreatePlayersOnLevelCommand : IRequest<IResult>
    {
        public string ProjectID { get; set; }
        public long TotalPlayerCount { get; set; }
        public int LevelIndex { get; set; }
        public DateTime DateTime { get; set; }
        public long PaidPlayerCount { get; set; }

        public class CreatePlayersOnLevelCommandHandler : IRequestHandler<CreatePlayersOnLevelCommand, IResult>
        {
            private readonly IPlayerCountsOnLevelRepository _playersOnLevelRepository;
            private readonly IMediator _mediator;

            public CreatePlayersOnLevelCommandHandler(IPlayerCountsOnLevelRepository playersOnLevelRepository, IMediator mediator)
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

                var addedPlayersOnLevel = new PlayerCountsOnLevel
                {
                    ProjectID = request.ProjectID,
                    PaidPlayerCount = request.PaidPlayerCount,
                    TotalPlayerCount = request.TotalPlayerCount,
                    LevelIndex = request.LevelIndex,
                    DateTime = request.DateTime
                };

                await _playersOnLevelRepository.AddAsync(addedPlayersOnLevel);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}