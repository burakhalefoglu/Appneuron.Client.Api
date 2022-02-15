using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class AdvEvent : IEntity
    {
        public long Id { get; set; }
        public long ClientId { get; set; }
        public long ProjectId { get; set; }
        public long CustomerId { get; set; }
        public string LevelName { get; set; }
        public int LevelIndex { get; set; }
        public string AdvType { get; set; }
        public float InMinutes { get; set; }
        public DateTime TriggerTime { get; set; }
        public bool Status = true;

    }
}