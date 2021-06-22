
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
using Business.Handlers.ProjectBaseSuccessAttemptRates.ValidationRules;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.ProjectBaseSuccessAttemptRates.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateProjectBaseSuccessAttemptRateCommand : IRequest<IResult>
    {

        public string ProjectId { get; set; }
        public SuccessAttemptWithDifficulty[] SuccessAttemptWithDifficulty { get; set; }


        public class CreateProjectBaseSuccessAttemptRateCommandHandler : IRequestHandler<CreateProjectBaseSuccessAttemptRateCommand, IResult>
        {
            private readonly IProjectBaseSuccessAttemptRateRepository _projectBaseSuccessAttemptRateRepository;
            private readonly IMediator _mediator;
            public CreateProjectBaseSuccessAttemptRateCommandHandler(IProjectBaseSuccessAttemptRateRepository projectBaseSuccessAttemptRateRepository, IMediator mediator)
            {
                _projectBaseSuccessAttemptRateRepository = projectBaseSuccessAttemptRateRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateProjectBaseSuccessAttemptRateValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateProjectBaseSuccessAttemptRateCommand request, CancellationToken cancellationToken)
            {
                var isThereProjectBaseSuccessAttemptRateRecord = _projectBaseSuccessAttemptRateRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isThereProjectBaseSuccessAttemptRateRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedProjectBaseSuccessAttemptRate = new SuccessAttemptRate
                {
                    ProjectId = request.ProjectId,
                    SuccessAttemptWithDifficulty = request.SuccessAttemptWithDifficulty

                };

                await _projectBaseSuccessAttemptRateRepository.AddAsync(addedProjectBaseSuccessAttemptRate);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}