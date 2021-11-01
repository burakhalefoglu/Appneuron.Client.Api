using Core.Entities;
using System;


namespace Entities.Concrete
{
    public class OfferBehaviorModel : DocumentDbEntity
    {
        public string ClientId { get; set; }
        public string ProjectId { get; set; }
        public string CustomerId { get; set; }
        public int Version { get; set; }
        public string OfferName { get; set; }
        public DateTime DateTime { get; set; }
        public int IsBuyOffer { get; set; }
    }
}
