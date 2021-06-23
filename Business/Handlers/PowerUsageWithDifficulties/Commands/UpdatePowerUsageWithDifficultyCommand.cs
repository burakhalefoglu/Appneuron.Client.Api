using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.PowerUsageWithDifficulties.ValidationRules;
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

namespace Business.Handlers.PowerUsageWithDifficulties.Commands
{
    public class UpdatePowerUsageWithDifficultyCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public int DifficultyLevel { get; set; }
        public long PowerUsageCount { get; set; }
        public DateTime DateTime { get; set; }

        public class UpdatePowerUsageWithDifficultyCommandHandler : IRequestHandler<UpdatePowerUsageWithDifficultyCommand, IResult>
        {
            private readonly IPowerUsageWithDifficultyRepository _powerUsageWithDifficultyRepository;
            private readonly IMediator _mediator;

            public UpdatePowerUsageWithDifficultyCommandHandler(IPowerUsageWithDifficultyRepository powerUsageWithDifficultyRepository, IMediator mediator)
            {
                _powerUsageWithDifficultyRepository = powerUsageWithDifficultyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdatePowerUsageWithDifficultyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdatePowerUsageWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                var powerUsageWithDifficulty = new PowerUsageWithDifficulty();
                powerUsageWithDifficulty.ProjectId = request.ProjectId;
                powerUsageWithDifficulty.PowerUsageCount = request.PowerUsageCount;
                powerUsageWithDifficulty.DateTime = request.DateTime;
                powerUsageWithDifficulty.DifficultyLevel = request.DifficultyLevel;

                await _powerUsageWithDifficultyRepository.UpdateAsync(request.Id, powerUsageWithDifficulty);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}