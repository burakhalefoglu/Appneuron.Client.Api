﻿using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class OfferBehaviorModel : IEntity
    {
        public long Id { get; set; }
        public long ClientId { get; set; }
        public long ProjectId { get; set; }
        public long CustomerId { get; set; }
        public short Version { get; set; }
        public int OfferId { get; set; }
        public byte IsBuyOffer { get; set; }
        public DateTime DateTime { get; set; }
        
        public bool Status = true;

    }
}