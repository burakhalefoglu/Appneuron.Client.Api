using Core.Entities;

namespace Entities.Dtos;

public class OfferBehaviorDto : IDto
{
    public int Version { get; set; }
    public int OfferId { get; set; }
    public int IsBuyOffer { get; set; }
}