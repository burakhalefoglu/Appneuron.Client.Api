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

namespace Business.Handlers.EnemyBaseLoginLevelModels.Queries
{
    public class GetEnemyBaseLoginLevelModelByProjectIdQuery : IRequest<IDataResult<IEnumerable<EnemyBaseLoginLevelModel>>>
    {
        public long ProjectId { get; set; }

        public class GetEnemyBaseLoginLevelModelByProjectIdQueryHandler : IRequestHandler<
            GetEnemyBaseLoginLevelModelByProjectIdQuery, IDataResult<IEnumerable<EnemyBaseLoginLevelModel>>>
        {
            private readonly IEnemyBaseLoginLevelModelRepository _everyLoginLevelDataRepository;
            private readonly IMediator _mediator;

            public GetEnemyBaseLoginLevelModelByProjectIdQueryHandler(
                IEnemyBaseLoginLevelModelRepository everyLoginLevelDataRepository, IMediator mediator)
            {
                _everyLoginLevelDataRepository = everyLoginLevelDataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<EnemyBaseLoginLevelModel>>> Handle(
                GetEnemyBaseLoginLevelModelByProjectIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<EnemyBaseLoginLevelModel>>
                (await _everyLoginLevelDataRepository.GetListAsync(p =>
                    p.ProjectId == request.ProjectId && p.Status == true));
            }
        }
    }
}