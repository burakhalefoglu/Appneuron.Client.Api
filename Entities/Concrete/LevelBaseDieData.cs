using Core.Entities;
using System;

namespace Entities.Concrete
{
    public class LevelBaseDieData : DocumentDbEntity
    {
        public string ClientId { get; set; }
        public string ProjectId { get; set; } 
        public string CustomerId { get; set; }
        public int DiyingTimeAfterLevelStarting { get; set; }
        public string LevelName { get; set; }
        public int DiyingDifficultyLevel { get; set; }
        public float DiyingLocationX { get; set; }
        public float DiyingLocationY { get; set; }
        public float DiyingLocationZ { get; set; }
        public DateTime DateTime { get; set; }

    }
}