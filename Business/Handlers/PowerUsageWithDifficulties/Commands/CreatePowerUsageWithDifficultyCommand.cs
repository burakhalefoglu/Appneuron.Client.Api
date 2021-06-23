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
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PowerUsageWithDifficulties.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class CreatePowerUsageWithDifficultyCommand : IRequest<IResult>
    {
        public string ProjectId { get; set; }
        public int DifficultyLevel { get; set; }
        public long PowerUsageCount { get; set; }
        public DateTime DateTime { get; set; }

        public class CreatePowerUsageWithDifficultyCommandHandler : IRequestHandler<CreatePowerUsageWithDifficultyCommand, IResult>
        {
            private readonly IPowerUsageWithDifficultyRepository _powerUsageWithDifficultyRepository;
            private readonly IMediator _mediator;

            public CreatePowerUsageWithDifficultyCommandHandler(IPowerUsageWithDifficultyRepository powerUsageWithDifficultyRepository, IMediator mediator)
            {
                _powerUsageWithDifficultyRepository = powerUsageWithDifficultyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreatePowerUsageWithDifficultyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreatePowerUsageWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                var isTherePowerUsageWithDifficultyRecord = _powerUsageWithDifficultyRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isTherePowerUsageWithDifficultyRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedPowerUsageWithDifficulty = new PowerUsageWithDifficulty
                {
                    ProjectId = request.ProjectId,
                    DateTime = request.DateTime,
                    DifficultyLevel = request.DifficultyLevel,
                    PowerUsageCount = request.PowerUsageCount
                };

                await _powerUsageWithDifficultyRepository.AddAsync(addedPowerUsageWithDifficulty);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}