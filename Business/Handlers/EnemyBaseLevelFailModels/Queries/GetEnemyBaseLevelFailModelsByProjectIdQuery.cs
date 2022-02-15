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

namespace Business.Handlers.EnemyBaseLevelFailModels.Queries
{
    public class GetEnemyBaseLevelFailModelsByProjectIdQuery : IRequest<IDataResult<IEnumerable<EnemyBaseLevelFailModel>>>
    {
        public long ProjectId { get; set; }

        public class GetEnemyBaseLevelFailModelsByProjectIdQueryHandler : IRequestHandler<GetEnemyBaseLevelFailModelsByProjectIdQuery,
            IDataResult<IEnumerable<EnemyBaseLevelFailModel>>>
        {
            private readonly IEnemyBaseLevelFailModelRepository _levelBaseDieDataRepository;

            public GetEnemyBaseLevelFailModelsByProjectIdQueryHandler(IEnemyBaseLevelFailModelRepository levelBaseDieDataRepository)
            {
                _levelBaseDieDataRepository = levelBaseDieDataRepository;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<EnemyBaseLevelFailModel>>> Handle(
                GetEnemyBaseLevelFailModelsByProjectIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<EnemyBaseLevelFailModel>>
                (await _levelBaseDieDataRepository.GetListAsync(p =>
                    p.ProjectId == request.ProjectId && p.Status == true));
            }
        }
    }
}