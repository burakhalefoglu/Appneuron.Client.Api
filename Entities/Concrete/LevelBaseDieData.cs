using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class LevelBaseDieData : IEntity
    {
        public bool Status = true;
        public long ClientId { get; set; }
        public long ProjectId { get; set; }
        public long CustomerId { get; set; }
        public int DiyingTimeAfterLevelStarting { get; set; }
        public string LevelName { get; set; }
        public int DiyingDifficultyLevel { get; set; }
        public float DiyingLocationX { get; set; }
        public float DiyingLocationY { get; set; }
        public float DiyingLocationZ { get; set; }
        public DateTime DateTime { get; set; }
        public long Id { get; set; }
    }
}