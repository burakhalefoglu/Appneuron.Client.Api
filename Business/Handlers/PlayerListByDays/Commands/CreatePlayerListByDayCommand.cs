
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
using Business.Handlers.PlayerListByDays.ValidationRules;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.PlayerListByDays.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreatePlayerListByDayCommand : IRequest<IResult>
    {

        public string ProjectId { get; set; }
        public System.DateTime DateTime { get; set; }
        public PlayerListWithDifficulty[] PlayerListWithDifficulty { get; set; }


        public class CreatePlayerListByDayCommandHandler : IRequestHandler<CreatePlayerListByDayCommand, IResult>
        {
            private readonly IPlayerListByDayRepository _playerListByDayRepository;
            private readonly IMediator _mediator;
            public CreatePlayerListByDayCommandHandler(IPlayerListByDayRepository playerListByDayRepository, IMediator mediator)
            {
                _playerListByDayRepository = playerListByDayRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreatePlayerListByDayValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreatePlayerListByDayCommand request, CancellationToken cancellationToken)
            {
                var isTherePlayerListByDayRecord = _playerListByDayRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isTherePlayerListByDayRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedPlayerListByDay = new ProjectBasePlayerListByDayWithDifficulty
                {
                    ProjectId = request.ProjectId,
                    DateTime = request.DateTime,
                    PlayerListWithDifficulty = request.PlayerListWithDifficulty

                };

                await _playerListByDayRepository.AddAsync(addedPlayerListByDay);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}