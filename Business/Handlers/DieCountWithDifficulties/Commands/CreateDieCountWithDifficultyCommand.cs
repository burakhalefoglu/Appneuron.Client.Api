using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.DieCountWithDifficulties.ValidationRules;
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

namespace Business.Handlers.DieCountWithDifficulties.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class CreateDieCountWithDifficultyCommand : IRequest<IResult>
    {
        public string ProjectId { get; set; }
        public int DifficultyLevel { get; set; }
        public long TotalDie { get; set; }
        public DateTime DateTime { get; set; }

        public class CreateDieCountWithDifficultyCommandHandler : IRequestHandler<CreateDieCountWithDifficultyCommand, IResult>
        {
            private readonly IDieCountWithDifficultyRepository _dieCountWithDifficultyRepository;
            private readonly IMediator _mediator;

            public CreateDieCountWithDifficultyCommandHandler(IDieCountWithDifficultyRepository dieCountWithDifficultyRepository, IMediator mediator)
            {
                _dieCountWithDifficultyRepository = dieCountWithDifficultyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateDieCountWithDifficultyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateDieCountWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                var isThereDieCountWithDifficultyRecord = _dieCountWithDifficultyRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isThereDieCountWithDifficultyRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedDieCountWithDifficulty = new DieCountWithDifficulty
                {
                    ProjectId = request.ProjectId,
                    DateTime = request.DateTime,
                    DifficultyLevel = request.DifficultyLevel,
                    TotalDie = request.TotalDie
                };

                await _dieCountWithDifficultyRepository.AddAsync(addedDieCountWithDifficulty);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}