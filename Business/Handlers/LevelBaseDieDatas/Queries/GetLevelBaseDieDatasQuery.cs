using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.LevelBaseDieDatas.Queries
{
    public class GetLevelBaseDieDatasQuery : IRequest<IDataResult<IEnumerable<LevelBaseDieData>>>
    {
        public class GetLevelBaseDieDatasQueryHandler : IRequestHandler<GetLevelBaseDieDatasQuery, IDataResult<IEnumerable<LevelBaseDieData>>>
        {
            private readonly ILevelBaseDieDataRepository _levelBaseDieDataRepository;
            private readonly IMediator _mediator;

            public GetLevelBaseDieDatasQueryHandler(ILevelBaseDieDataRepository levelBaseDieDataRepository, IMediator mediator)
            {
                _levelBaseDieDataRepository = levelBaseDieDataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<LevelBaseDieData>>> Handle(GetLevelBaseDieDatasQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<LevelBaseDieData>>(await _levelBaseDieDataRepository.GetListAsync());
            }
        }
    }
}