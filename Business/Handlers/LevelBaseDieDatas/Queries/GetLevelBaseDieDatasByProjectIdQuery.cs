using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.LevelBaseDieDatas.Queries
{
    public class GetLevelBaseDieDatasByProjectIdQuery : IRequest<IDataResult<IEnumerable<LevelBaseDieData>>>
    {
        public long ProjectId { get; set; }

        public class GetLevelBaseDieDatasByProjectIdQueryHandler : IRequestHandler<GetLevelBaseDieDatasByProjectIdQuery,
            IDataResult<IEnumerable<LevelBaseDieData>>>
        {
            private readonly ILevelBaseDieDataRepository _levelBaseDieDataRepository;

            public GetLevelBaseDieDatasByProjectIdQueryHandler(ILevelBaseDieDataRepository levelBaseDieDataRepository)
            {
                _levelBaseDieDataRepository = levelBaseDieDataRepository;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<LevelBaseDieData>>> Handle(
                GetLevelBaseDieDatasByProjectIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<LevelBaseDieData>>
                (await _levelBaseDieDataRepository.GetListAsync(p =>
                    p.ProjectId == request.ProjectId && p.Status == true));
            }
        }
    }
}