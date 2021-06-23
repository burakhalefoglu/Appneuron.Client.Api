using Core.Entities;
using System;

namespace Entities.Concrete.ChartModels
{
    public class DieCountWithDifficulty : DocumentDbEntity
    {
        public string ProjectId { get; set; }
        public int DifficultyLevel { get; set; }
        public long TotalDie { get; set; }
        public DateTime DateTime { get; set; }
    }
}