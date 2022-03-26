using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos;
using MediatR;

namespace Business.Handlers.OfferBehaviorModels.Queries
{
    public class GetOfferBehaviorDtoQuery : IRequest<IDataResult<IEnumerable<OfferBehaviorDto>>>
    {
        public long ProjectId { get; set; }
        public int OfferId { get; set; }
        public int Version { get; set; }

        public class GetOfferBehaviorDtoQueryHandler : IRequestHandler<GetOfferBehaviorDtoQuery,
            IDataResult<IEnumerable<OfferBehaviorDto>>>
        {
            private readonly IOfferBehaviorModelRepository _offerBehaviorModelRepository;

            public GetOfferBehaviorDtoQueryHandler(IOfferBehaviorModelRepository offerBehaviorModelRepository)
            {
                _offerBehaviorModelRepository = offerBehaviorModelRepository;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OfferBehaviorDto>>> Handle(GetOfferBehaviorDtoQuery request,
                CancellationToken cancellationToken)
            {
                var offerDtoList = new List<OfferBehaviorDto>();
                var offerResult = await _offerBehaviorModelRepository.GetListAsync(
                    o => o.ProjectId == request.ProjectId &&
                         o.OfferId == request.OfferId &&
                         o.Version == request.Version && o.Status == true);
                offerResult.ToList().ForEach(o =>
                {
                    offerDtoList.Add(new OfferBehaviorDto
                    {
                        IsBuyOffer = o.IsBuyOffer,
                        Version = o.Version,
                        OfferId = o.OfferId
                    });
                });
                return new SuccessDataResult<IEnumerable<OfferBehaviorDto>>(offerDtoList);
            }
        }
    }
}