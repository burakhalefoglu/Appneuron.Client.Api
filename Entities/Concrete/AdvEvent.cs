using Core.Entities;
using System;

namespace Entities.Concrete
{ 
    public class AdvEvent : IEntity
    {
        public long ClientId { get; set; }
        public long ProjectId { get; set; }
        public long CustomerId { get; set; }
        public string TriggersLevelName { get; set; }
        public string AdvType { get; set; }
        public int DifficultyLevel { get; set; }
        public float InMinutes { get; set; }
        public DateTime TriggerTime { get; set; }
        public bool Status = true;
        public long Id { get; set; }
    }
}