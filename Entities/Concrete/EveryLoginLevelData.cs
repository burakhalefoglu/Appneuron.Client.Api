using Core.Entities;
using System;

namespace Entities.Concrete
{
    public class EveryLoginLevelData : DocumentDbEntity
    {
        public string ClientId { get; set; }
        public string ProjectId { get; set; }
        public string CustomerId { get; set; }
        public string Levelname { get; set; }
        public int LevelsDifficultylevel { get; set; }
        public int PlayingTime { get; set; }
        public int AverageScores { get; set; }
        public DateTime DateTime { get; set; }
        public int IsDead { get; set; }
        public int TotalPowerUsage { get; set; }

    }
}