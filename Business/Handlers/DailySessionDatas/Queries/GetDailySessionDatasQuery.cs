
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

namespace Business.Handlers.DailySessionDatas.Queries
{

    public class GetDailySessionDatasQuery : IRequest<IDataResult<IEnumerable<DailySessionData>>>
    {
        public class GetDailySessionDatasQueryHandler : IRequestHandler<GetDailySessionDatasQuery, IDataResult<IEnumerable<DailySessionData>>>
        {
            private readonly IDailySessionDataRepository _dailySessionDataRepository;
            private readonly IMediator _mediator;

            public GetDailySessionDatasQueryHandler(IDailySessionDataRepository dailySessionDataRepository, IMediator mediator)
            {
                _dailySessionDataRepository = dailySessionDataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<DailySessionData>>> Handle(GetDailySessionDatasQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<DailySessionData>>(await _dailySessionDataRepository.GetListAsync());
            }
        }
    }
}