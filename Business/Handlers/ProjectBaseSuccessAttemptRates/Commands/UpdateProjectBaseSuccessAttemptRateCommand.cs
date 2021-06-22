
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.ProjectBaseSuccessAttemptRates.ValidationRules;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.ProjectBaseSuccessAttemptRates.Commands
{


    public class UpdateProjectBaseSuccessAttemptRateCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public SuccessAttemptWithDifficulty[] SuccessAttemptWithDifficulty { get; set; }

        public class UpdateProjectBaseSuccessAttemptRateCommandHandler : IRequestHandler<UpdateProjectBaseSuccessAttemptRateCommand, IResult>
        {
            private readonly IProjectBaseSuccessAttemptRateRepository _projectBaseSuccessAttemptRateRepository;
            private readonly IMediator _mediator;

            public UpdateProjectBaseSuccessAttemptRateCommandHandler(IProjectBaseSuccessAttemptRateRepository projectBaseSuccessAttemptRateRepository, IMediator mediator)
            {
                _projectBaseSuccessAttemptRateRepository = projectBaseSuccessAttemptRateRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateProjectBaseSuccessAttemptRateValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateProjectBaseSuccessAttemptRateCommand request, CancellationToken cancellationToken)
            {



                var projectBaseSuccessAttemptRate = new SuccessAttemptRate();
                projectBaseSuccessAttemptRate.ProjectId = request.ProjectId;
                projectBaseSuccessAttemptRate.SuccessAttemptWithDifficulty = request.SuccessAttemptWithDifficulty;

                await _projectBaseSuccessAttemptRateRepository.UpdateAsync(request.Id, projectBaseSuccessAttemptRate);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

