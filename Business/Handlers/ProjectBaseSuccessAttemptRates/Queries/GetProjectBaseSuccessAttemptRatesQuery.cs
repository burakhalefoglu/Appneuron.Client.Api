
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;
using Entities.Concrete.ChartModels;

namespace Business.Handlers.ProjectBaseSuccessAttemptRates.Queries
{

    public class GetProjectBaseSuccessAttemptRatesQuery : IRequest<IDataResult<IEnumerable<SuccessAttemptRate>>>
    {
        public class GetProjectBaseSuccessAttemptRatesQueryHandler : IRequestHandler<GetProjectBaseSuccessAttemptRatesQuery, IDataResult<IEnumerable<SuccessAttemptRate>>>
        {
            private readonly IProjectBaseSuccessAttemptRateRepository _projectBaseSuccessAttemptRateRepository;
            private readonly IMediator _mediator;

            public GetProjectBaseSuccessAttemptRatesQueryHandler(IProjectBaseSuccessAttemptRateRepository projectBaseSuccessAttemptRateRepository, IMediator mediator)
            {
                _projectBaseSuccessAttemptRateRepository = projectBaseSuccessAttemptRateRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<SuccessAttemptRate>>> Handle(GetProjectBaseSuccessAttemptRatesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<SuccessAttemptRate>>(await _projectBaseSuccessAttemptRateRepository.GetListAsync());
            }
        }
    }
}