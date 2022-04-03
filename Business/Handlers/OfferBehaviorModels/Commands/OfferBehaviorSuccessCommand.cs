using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos;
using MediatR;

namespace Business.Handlers.OfferBehaviorModels.Commands;

public class OfferBehaviorSuccessCommand : IRequest<IDataResult<OfferBehaviorSuccessDto>>
{
    public long ProjectId { get; set; } 

    public OfferDto[] OfferDtos { get; set; }

    public class OfferBehaviorSuccessCommandHandler : IRequestHandler<OfferBehaviorSuccessCommand,
        IDataResult<OfferBehaviorSuccessDto>>
    {
        private readonly IOfferBehaviorModelRepository _offerBehaviorModelRepository;

        public OfferBehaviorSuccessCommandHandler(IOfferBehaviorModelRepository offerBehaviorModelRepository)
        {
            _offerBehaviorModelRepository = offerBehaviorModelRepository;
        }

        [PerformanceAspect(5)]
        [CacheAspect(10)]
        [LogAspect(typeof(ConsoleLogger))]
        [SecuredOperation(Priority = 1)]
        public async Task<IDataResult<OfferBehaviorSuccessDto>> Handle(OfferBehaviorSuccessCommand request,
            CancellationToken cancellationToken)
        {
            var offerDto = new OfferBehaviorSuccessDto();

            foreach (var requestOfferDto in request.OfferDtos)
            {
                var offerResult = await _offerBehaviorModelRepository.GetListAsync(
                    o => o.ProjectId == request.ProjectId &&
                         o.OfferId == requestOfferDto.Id);
                var totalOffer = offerResult.Count();
                var successOffer =
                    offerResult.Count(x => x.IsBuyOffer == 1);
                if (totalOffer == 0 || successOffer == 0)
                {
                    offerDto.OfferNames.Add(requestOfferDto.Name + requestOfferDto.Version);
                    offerDto.SuccessPercents.Add(0);
                    continue;
                }
                var offerSuccessPercent = successOffer * 100 / totalOffer;
                offerDto.OfferNames.Add(requestOfferDto.Name);
                offerDto.SuccessPercents.Add(offerSuccessPercent);
            }

            return new SuccessDataResult<OfferBehaviorSuccessDto>(offerDto);
        }
    }
}