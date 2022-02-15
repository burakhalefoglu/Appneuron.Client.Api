using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class EnemyBaseLevelFailModel : IEntity
    {        
        public long Id { get; set; }
        public long ClientId { get; set; }
        public long ProjectId { get; set; }
        public long CustomerId { get; set; }
        public string LevelName { get; set; }
        public int LevelIndex { get; set; }
        public int FailTimeAfterLevelStarting { get; set; }
        public float FailLocationX { get; set; }
        public float FailLocationY { get; set; }
        public float FailLocationZ { get; set; }
        public DateTime DateTime { get; set; }
        public bool Status = true;

    }
}