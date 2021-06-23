using Core.Entities;
using System;

namespace Entities.Concrete.ChartModels
{
    public class LevelBasePlayerCountWithDifficulty : DocumentDbEntity
    {
        public string ProjectId { get; set; }
        public int LevelIndex { get; set; }
        public DateTime DateTime { get; set; }
        public long PlayerCount { get; set; }
        public int DifficultyLevel { get; set; }
    }
}