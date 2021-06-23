using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.SuccessAttemptRateWithDifficulties.ValidationRules;
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

namespace Business.Handlers.SuccessAttemptRateWithDifficulties.Commands
{
    public class UpdateSuccessAttemptRateWithDifficultyCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public int DifficultyLevel { get; set; }
        public long SuccessAttempt { get; set; }
        public DateTime DateTime { get; set; }

        public class UpdateSuccessAttemptRateWithDifficultyCommandHandler : IRequestHandler<UpdateSuccessAttemptRateWithDifficultyCommand, IResult>
        {
            private readonly ISuccessAttemptRateWithDifficultyRepository _successAttemptRateWithDifficultyRepository;
            private readonly IMediator _mediator;

            public UpdateSuccessAttemptRateWithDifficultyCommandHandler(ISuccessAttemptRateWithDifficultyRepository successAttemptRateWithDifficultyRepository, IMediator mediator)
            {
                _successAttemptRateWithDifficultyRepository = successAttemptRateWithDifficultyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateSuccessAttemptRateWithDifficultyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateSuccessAttemptRateWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                var successAttemptRateWithDifficulty = new SuccessAttemptRateWithDifficulty();
                successAttemptRateWithDifficulty.ProjectId = request.ProjectId;
                successAttemptRateWithDifficulty.SuccessAttempt = request.SuccessAttempt;
                successAttemptRateWithDifficulty.DateTime = request.DateTime;
                successAttemptRateWithDifficulty.DifficultyLevel = request.DifficultyLevel;

                await _successAttemptRateWithDifficultyRepository.UpdateAsync(request.Id, successAttemptRateWithDifficulty);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}