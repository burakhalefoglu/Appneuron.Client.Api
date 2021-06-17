﻿
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
using Business.Handlers.PlayersOnLevels.ValidationRules;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.PlayersOnLevels.Commands
{


    public class UpdatePlayersOnLevelCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectID { get; set; }
        public PlayerCountOnLevel[] PlayerCountOnLevel { get; set; }

        public class UpdatePlayersOnLevelCommandHandler : IRequestHandler<UpdatePlayersOnLevelCommand, IResult>
        {
            private readonly IPlayersOnLevelRepository _playersOnLevelRepository;
            private readonly IMediator _mediator;

            public UpdatePlayersOnLevelCommandHandler(IPlayersOnLevelRepository playersOnLevelRepository, IMediator mediator)
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



                var playersOnLevel = new ProjectBasePlayerCountsOnLevel();
                playersOnLevel.ProjectID = request.ProjectID;
                playersOnLevel.PlayerCountOnLevel = request.PlayerCountOnLevel;


                await _playersOnLevelRepository.UpdateAsync(request.Id, playersOnLevel);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

