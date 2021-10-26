
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

namespace Business.Handlers.AdvStrategyBehaviorModels.Queries
{

    public class GetAdvStrategyBehaviorModelsQuery : IRequest<IDataResult<IEnumerable<AdvStrategyBehaviorModel>>>
    {
        public class GetAdvStrategyBehaviorModelsQueryHandler : IRequestHandler<GetAdvStrategyBehaviorModelsQuery, IDataResult<IEnumerable<AdvStrategyBehaviorModel>>>
        {
            private readonly IAdvStrategyBehaviorModelRepository _advStrategyBehaviorModelRepository;
            private readonly IMediator _mediator;

            public GetAdvStrategyBehaviorModelsQueryHandler(IAdvStrategyBehaviorModelRepository advStrategyBehaviorModelRepository, IMediator mediator)
            {
                _advStrategyBehaviorModelRepository = advStrategyBehaviorModelRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<AdvStrategyBehaviorModel>>> Handle(GetAdvStrategyBehaviorModelsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<AdvStrategyBehaviorModel>>(await _advStrategyBehaviorModelRepository.GetListAsync());
            }
        }
    }
}