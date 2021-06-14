
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

namespace Business.Handlers.EveryLoginLevelDatas.Queries
{

    public class GetEveryLoginLevelDatasQuery : IRequest<IDataResult<IEnumerable<EveryLoginLevelData>>>
    {
        public class GetEveryLoginLevelDatasQueryHandler : IRequestHandler<GetEveryLoginLevelDatasQuery, IDataResult<IEnumerable<EveryLoginLevelData>>>
        {
            private readonly IEveryLoginLevelDataRepository _everyLoginLevelDataRepository;
            private readonly IMediator _mediator;

            public GetEveryLoginLevelDatasQueryHandler(IEveryLoginLevelDataRepository everyLoginLevelDataRepository, IMediator mediator)
            {
                _everyLoginLevelDataRepository = everyLoginLevelDataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<EveryLoginLevelData>>> Handle(GetEveryLoginLevelDatasQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<EveryLoginLevelData>>(await _everyLoginLevelDataRepository.GetListAsync());
            }
        }
    }
}