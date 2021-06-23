using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.AdvClickWithDifficulties.ValidationRules;
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

namespace Business.Handlers.AdvClickWithDifficulties.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class CreateAdvClickWithDifficultyCommand : IRequest<IResult>
    {
        public string ProjectId { get; set; }
        public int DifficultyLevel { get; set; }
        public long AdvClick { get; set; }
        public DateTime DateTime { get; set; }

        public class CreateAdvClickWithDifficultyCommandHandler : IRequestHandler<CreateAdvClickWithDifficultyCommand, IResult>
        {
            private readonly IAdvClickWithDifficultyRepository _advClickWithDifficultyRepository;
            private readonly IMediator _mediator;

            public CreateAdvClickWithDifficultyCommandHandler(IAdvClickWithDifficultyRepository advClickWithDifficultyRepository, IMediator mediator)
            {
                _advClickWithDifficultyRepository = advClickWithDifficultyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateAdvClickWithDifficultyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateAdvClickWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                var isThereAdvClickWithDifficultyRecord = _advClickWithDifficultyRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isThereAdvClickWithDifficultyRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedAdvClickWithDifficulty = new AdvClickWithDifficulty
                {
                    ProjectId = request.ProjectId,
                    DifficultyLevel = request.DifficultyLevel,
                    DateTime = request.DateTime,
                    AdvClick = request.AdvClick
                };

                await _advClickWithDifficultyRepository.AddAsync(addedAdvClickWithDifficulty);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}