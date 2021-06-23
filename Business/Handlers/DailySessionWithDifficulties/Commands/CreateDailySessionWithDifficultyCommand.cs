using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.DailySessionWithDifficulties.ValidationRules;
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

namespace Business.Handlers.DailySessionWithDifficulties.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class CreateDailySessionWithDifficultyCommand : IRequest<IResult>
    {
        public string ProjectId { get; set; }
        public DateTime DateTimePerThreeHour { get; set; }
        public int AvarageTimeSession { get; set; }
        public int DifficultyLevel { get; set; }

        public class CreateDailySessionWithDifficultyCommandHandler : IRequestHandler<CreateDailySessionWithDifficultyCommand, IResult>
        {
            private readonly IDailySessionWithDifficultyRepository _dailySessionWithDifficultyRepository;
            private readonly IMediator _mediator;

            public CreateDailySessionWithDifficultyCommandHandler(IDailySessionWithDifficultyRepository dailySessionWithDifficultyRepository, IMediator mediator)
            {
                _dailySessionWithDifficultyRepository = dailySessionWithDifficultyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateDailySessionWithDifficultyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateDailySessionWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                var isThereDailySessionWithDifficultyRecord = _dailySessionWithDifficultyRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isThereDailySessionWithDifficultyRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedDailySessionWithDifficulty = new DailySessionWithDifficulty
                {
                    ProjectId = request.ProjectId,
                    DifficultyLevel = request.DifficultyLevel,
                    AvarageTimeSession = request.AvarageTimeSession,
                    DateTimePerThreeHour = request.DateTimePerThreeHour
                };

                await _dailySessionWithDifficultyRepository.AddAsync(addedDailySessionWithDifficulty);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}