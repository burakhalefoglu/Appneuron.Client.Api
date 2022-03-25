using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class LevelBaseSessionModel : IEntity
    {        
        public long Id { get; set; }
        public bool Status { get; set; }
        public long ClientId { get; set; }
        public long ProjectId { get; set; }
        public long CustomerId { get; set; }
        public string LevelName { get; set; }
        public int LevelIndex { get; set; }
        public float SessionTimeMinute { get; set; }
        public DateTime SessionStartTime { get; set; }
        public DateTime SessionFinishTime { get; set; }

    }
}