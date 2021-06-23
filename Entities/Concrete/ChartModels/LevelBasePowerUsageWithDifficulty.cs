using Core.Entities;
using System;

namespace Entities.Concrete.ChartModels
{
    public class LevelBasePowerUsageWithDifficulty : DocumentDbEntity
    {
        public string ProjectId { get; set; }
        public int DifficultyLevel { get; set; }
        public long PowerUsageCount { get; set; }
        public DateTime DateTime { get; set; }
        public int LevelIndex { get; set; }
    }
}