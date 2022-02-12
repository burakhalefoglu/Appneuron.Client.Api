using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class EveryLoginLevelData : IEntity
    {
        public bool Status = true;
        public long ClientId { get; set; }
        public long ProjectId { get; set; }
        public long CustomerId { get; set; }
        public string LevelName { get; set; }
        public int LevelsDifficultyLevel { get; set; }
        public int PlayingTime { get; set; }
        public int AverageScores { get; set; }
        public DateTime DateTime { get; set; }
        public byte IsDead { get; set; }
        public int TotalPowerUsage { get; set; }
        public long Id { get; set; }
    }
}