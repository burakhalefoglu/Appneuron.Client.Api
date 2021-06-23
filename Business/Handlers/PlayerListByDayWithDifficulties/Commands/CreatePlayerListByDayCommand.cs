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
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PlayerListByDaysWithDifficulty.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class CreatePlayerListByDayCommand : IRequest<IResult>
    {
        public string ProjectId { get; set; }
        public DateTime DateTime { get; set; }
        public string ClientId { get; set; }
        public int DifficultyLevel { get; set; }

        public class CreatePlayerListByDayCommandHandler : IRequestHandler<CreatePlayerListByDayCommand, IResult>
        {
            private readonly IPlayerListByDayWithDifficultyRepository _playerListByDayRepository;
            private readonly IMediator _mediator;

            public CreatePlayerListByDayCommandHandler(IPlayerListByDayWithDifficultyRepository playerListByDayRepository, IMediator mediator)
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

                var addedPlayerListByDay = new PlayerListByDayWithDifficulty
                {
                    ProjectId = request.ProjectId,
                    ClientId = request.ClientId,
                    DifficultyLevel = request.DifficultyLevel,
                    DateTime = request.DateTime
                };

                await _playerListByDayRepository.AddAsync(addedPlayerListByDay);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}