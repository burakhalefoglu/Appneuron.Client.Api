
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;

namespace Business.Handlers.ProjectBaseSuccessAttemptRates.Queries
{
    public class GetProjectBaseSuccessAttemptRateQuery : IRequest<IDataResult<SuccessAttemptRate>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetProjectBaseSuccessAttemptRateQueryHandler : IRequestHandler<GetProjectBaseSuccessAttemptRateQuery, IDataResult<SuccessAttemptRate>>
        {
            private readonly IProjectBaseSuccessAttemptRateRepository _projectBaseSuccessAttemptRateRepository;
            private readonly IMediator _mediator;

            public GetProjectBaseSuccessAttemptRateQueryHandler(IProjectBaseSuccessAttemptRateRepository projectBaseSuccessAttemptRateRepository, IMediator mediator)
            {
                _projectBaseSuccessAttemptRateRepository = projectBaseSuccessAttemptRateRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<SuccessAttemptRate>> Handle(GetProjectBaseSuccessAttemptRateQuery request, CancellationToken cancellationToken)
            {
                var projectBaseSuccessAttemptRate = await _projectBaseSuccessAttemptRateRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<SuccessAttemptRate>(projectBaseSuccessAttemptRate);
            }
        }
    }
}
