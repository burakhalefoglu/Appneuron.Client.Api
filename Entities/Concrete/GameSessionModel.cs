using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class GameSessionModel : IEntity
    {        
        public long Id { get; set; }
        public bool Status { get; set; }
        public long ClientId { get; set; }
        public long ProjectId { get; set; }
        public DateTimeOffset SessionStartTime { get; set; }
        public DateTimeOffset SessionFinishTime { get; set; }
        public float SessionTime { get; set; }

    }
}