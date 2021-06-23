using Core.Entities;
using System;

namespace Entities.Concrete.ChartModels
{
    public class LevelBaseDieCountWithDifficulty : DocumentDbEntity
    {
        public string ProjectId { get; set; }
        public int LevelIndex { get; set; }
        public long DieCount { get; set; }
        public DateTime DateTime { get; set; }
        public int DifficultyLevel { get; set; }
    }
}