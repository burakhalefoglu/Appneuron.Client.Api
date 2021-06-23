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
using MongoDB.Bson;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.DailySessionWithDifficulties.Commands
{
    public class UpdateDailySessionWithDifficultyCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public DateTime DateTimePerThreeHour { get; set; }
        public int AvarageTimeSession { get; set; }
        public int DifficultyLevel { get; set; }

        public class UpdateDailySessionWithDifficultyCommandHandler : IRequestHandler<UpdateDailySessionWithDifficultyCommand, IResult>
        {
            private readonly IDailySessionWithDifficultyRepository _dailySessionWithDifficultyRepository;
            private readonly IMediator _mediator;

            public UpdateDailySessionWithDifficultyCommandHandler(IDailySessionWithDifficultyRepository dailySessionWithDifficultyRepository, IMediator mediator)
            {
                _dailySessionWithDifficultyRepository = dailySessionWithDifficultyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateDailySessionWithDifficultyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateDailySessionWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                var dailySessionWithDifficulty = new DailySessionWithDifficulty();
                dailySessionWithDifficulty.ProjectId = request.ProjectId;
                dailySessionWithDifficulty.AvarageTimeSession = request.AvarageTimeSession;
                dailySessionWithDifficulty.DateTimePerThreeHour = request.DateTimePerThreeHour;
                dailySessionWithDifficulty.DifficultyLevel = request.DifficultyLevel;

                await _dailySessionWithDifficultyRepository.UpdateAsync(request.Id, dailySessionWithDifficulty);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}