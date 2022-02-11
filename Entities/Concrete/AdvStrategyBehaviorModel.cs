using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class AdvStrategyBehaviorModel : IEntity
    {
        public long ClientId { get; set; }
        public long ProjectId { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public long Id { get; set; }
        public bool Status = true;
    }
}
