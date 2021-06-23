using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.PlayerListByDaysWithDifficulty.ValidationRules;
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

namespace Business.Handlers.PlayerListByDaysWithDifficulty.Commands
{
    public class UpdatePlayerListByDayCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public DateTime DateTime { get; set; }
        public string ClientId { get; set; }
        public int DifficultyLevel { get; set; }

        public class UpdatePlayerListByDayCommandHandler : IRequestHandler<UpdatePlayerListByDayCommand, IResult>
        {
            private readonly IPlayerListByDayWithDifficultyRepository _playerListByDayRepository;
            private readonly IMediator _mediator;

            public UpdatePlayerListByDayCommandHandler(IPlayerListByDayWithDifficultyRepository playerListByDayRepository, IMediator mediator)
            {
                _playerListByDayRepository = playerListByDayRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdatePlayerListByDayValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdatePlayerListByDayCommand request, CancellationToken cancellationToken)
            {
                var playerListByDay = new PlayerListByDayWithDifficulty();
                playerListByDay.ProjectId = request.ProjectId;
                playerListByDay.ClientId = request.ClientId;
                playerListByDay.DateTime = request.DateTime;
                playerListByDay.DifficultyLevel = request.DifficultyLevel;

                await _playerListByDayRepository.UpdateAsync(request.Id, playerListByDay);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}