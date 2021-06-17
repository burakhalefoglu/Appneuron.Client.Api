
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
using Business.Handlers.PlayerListByDays.ValidationRules;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.PlayerListByDays.Commands
{


    public class UpdatePlayerListByDayCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public System.DateTime DateTime { get; set; }
        public PlayerListWithDifficulty[] PlayerListWithDifficulty { get; set; }

        public class UpdatePlayerListByDayCommandHandler : IRequestHandler<UpdatePlayerListByDayCommand, IResult>
        {
            private readonly IPlayerListByDayRepository _playerListByDayRepository;
            private readonly IMediator _mediator;

            public UpdatePlayerListByDayCommandHandler(IPlayerListByDayRepository playerListByDayRepository, IMediator mediator)
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



                var playerListByDay = new ProjectBasePlayerListByDayWithDifficulty();
                playerListByDay.ProjectId = request.ProjectId;
                playerListByDay.DateTime = request.DateTime;
                playerListByDay.PlayerListWithDifficulty = request.PlayerListWithDifficulty;


                await _playerListByDayRepository.UpdateAsync(request.Id, playerListByDay);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

