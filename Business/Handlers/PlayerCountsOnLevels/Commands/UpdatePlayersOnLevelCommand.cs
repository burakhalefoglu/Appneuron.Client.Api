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
using MongoDB.Bson;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PlayerCountsOnLevels.Commands
{
    public class UpdatePlayersOnLevelCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectID { get; set; }
        public long TotalPlayerCount { get; set; }
        public int LevelIndex { get; set; }
        public DateTime DateTime { get; set; }
        public long PaidPlayerCount { get; set; }

        public class UpdatePlayersOnLevelCommandHandler : IRequestHandler<UpdatePlayersOnLevelCommand, IResult>
        {
            private readonly IPlayerCountsOnLevelRepository _playersOnLevelRepository;
            private readonly IMediator _mediator;

            public UpdatePlayersOnLevelCommandHandler(IPlayerCountsOnLevelRepository playersOnLevelRepository, IMediator mediator)
            {
                _playersOnLevelRepository = playersOnLevelRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdatePlayersOnLevelValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdatePlayersOnLevelCommand request, CancellationToken cancellationToken)
            {
                var playersOnLevel = new PlayerCountsOnLevel();
                playersOnLevel.ProjectID = request.ProjectID;
                playersOnLevel.DateTime = request.DateTime;
                playersOnLevel.TotalPlayerCount = request.TotalPlayerCount;
                playersOnLevel.LevelIndex = request.LevelIndex;
                playersOnLevel.PaidPlayerCount = request.PaidPlayerCount;

                await _playersOnLevelRepository.UpdateAsync(request.Id, playersOnLevel);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}