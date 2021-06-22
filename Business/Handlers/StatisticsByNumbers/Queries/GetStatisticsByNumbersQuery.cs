
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

namespace Business.Handlers.StatisticsByNumbers.Queries
{

    public class GetStatisticsByNumbersQuery : IRequest<IDataResult<IEnumerable<StatisticsByNumber>>>
    {
        public class GetStatisticsByNumbersQueryHandler : IRequestHandler<GetStatisticsByNumbersQuery, IDataResult<IEnumerable<StatisticsByNumber>>>
        {
            private readonly IStatisticsByNumberRepository _statisticsByNumberRepository;
            private readonly IMediator _mediator;

            public GetStatisticsByNumbersQueryHandler(IStatisticsByNumberRepository statisticsByNumberRepository, IMediator mediator)
            {
                _statisticsByNumberRepository = statisticsByNumberRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<StatisticsByNumber>>> Handle(GetStatisticsByNumbersQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<StatisticsByNumber>>(await _statisticsByNumberRepository.GetListAsync());
            }
        }
    }
}