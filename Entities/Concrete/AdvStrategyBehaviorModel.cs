using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class AdvStrategyBehaviorModel : DocumentDbEntity
    {
        public string ClientId { get; set; }
        public string ProjectId { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public DateTime dateTime { get; set; }
    }
}
