using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class GameSessionModel : IEntity
    {        
        public long Id { get; set; }
        public long ClientId { get; set; }
        public long ProjectId { get; set; }
        public long CustomerId { get; set; }
        public DateTime SessionStartTime { get; set; }
        public DateTime SessionFinishTime { get; set; }
        public float SessionTimeMinute { get; set; }
        public bool Status = true;

    }
}