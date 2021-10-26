using Core.Entities;

namespace Entities.Dtos
{
    public class OfferBehaviorDto : IDto
    {
        public int Version { get; set; }
        public string OfferName { get; set; }
        public int IsBuyOffer { get; set; }

    }
}
