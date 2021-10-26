
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

namespace Business.Handlers.OfferBehaviorModels.Queries
{

    public class GetOfferBehaviorModelsQuery : IRequest<IDataResult<IEnumerable<OfferBehaviorModel>>>
    {
        public class GetOfferBehaviorModelsQueryHandler : IRequestHandler<GetOfferBehaviorModelsQuery, IDataResult<IEnumerable<OfferBehaviorModel>>>
        {
            private readonly IOfferBehaviorModelRepository _offerBehaviorModelRepository;
            private readonly IMediator _mediator;

            public GetOfferBehaviorModelsQueryHandler(IOfferBehaviorModelRepository offerBehaviorModelRepository, IMediator mediator)
            {
                _offerBehaviorModelRepository = offerBehaviorModelRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OfferBehaviorModel>>> Handle(GetOfferBehaviorModelsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OfferBehaviorModel>>(await _offerBehaviorModelRepository.GetListAsync());
            }
        }
    }
}