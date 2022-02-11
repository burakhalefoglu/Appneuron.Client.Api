using Core.Entities;
using System;


namespace Entities.Concrete
{
    public class OfferBehaviorModel : IEntity
    {
        public long ClientId { get; set; }
        public long ProjectId { get; set; }
        public long CustomerId { get; set; }
        public short Version { get; set; }
        public string OfferName { get; set; }
        public DateTime DateTime { get; set; }
        public byte IsBuyOffer { get; set; }
        public long Id { get; set; }
        public bool Status = true;
    }
}
