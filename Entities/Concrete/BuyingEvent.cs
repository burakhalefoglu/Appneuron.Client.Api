using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class BuyingEvent : IEntity
    {
        public bool Status = true;
        public long ClientId { get; set; }
        public long ProjectId { get; set; }
        public long CustomerId { get; set; }
        public string TriggersLevelName { get; set; }
        public string ProductType { get; set; }
        public int DifficultyLevel { get; set; }
        public float InMinutes { get; set; }
        public DateTime TriggeredTime { get; set; }
        public long Id { get; set; }
    }
}