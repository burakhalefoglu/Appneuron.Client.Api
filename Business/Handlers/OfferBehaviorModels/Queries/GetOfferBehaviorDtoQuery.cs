
using Business.BusinessAspects;
using Core.Utilities.Results;
using Core.Aspects.Autofac.Performance;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Caching;
using Entities.Dtos;
using System.Linq;

namespace Business.Handlers.OfferBehaviorModels.Queries
{

    public class GetOfferBehaviorDtoQuery : IRequest<IDataResult<IEnumerable<OfferBehaviorDto>>>
    {
        public string ProjectId { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }

        public class GetOfferBehaviorDtoQueryHandler : IRequestHandler<GetOfferBehaviorDtoQuery, IDataResult<IEnumerable<OfferBehaviorDto>>>
        {
            private readonly IOfferBehaviorModelRepository _offerBehaviorModelRepository;
            private readonly IMediator _mediator;

            public GetOfferBehaviorDtoQueryHandler(IOfferBehaviorModelRepository offerBehaviorModelRepository, IMediator mediator)
            {
                _offerBehaviorModelRepository = offerBehaviorModelRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OfferBehaviorDto>>> Handle(GetOfferBehaviorDtoQuery request, CancellationToken cancellationToken)
            {
                var offerDtoList = new List<OfferBehaviorDto>();
                var offerResult = await _offerBehaviorModelRepository.GetListAsync(
                    o => o.ProjectId == request.ProjectId &&
                    o.OfferName == request.Name &&
                    o.Version == request.Version);
                offerResult.ToList().ForEach(o =>
                {
                    offerDtoList.Add(new OfferBehaviorDto
                    {
                        IsBuyOffer = o.IsBuyOffer,
                        Version = o.Version,
                        OfferName = o.OfferName
                    });
                });
                return new SuccessDataResult<IEnumerable<OfferBehaviorDto>>(offerDtoList);
            }
        }
    }
}