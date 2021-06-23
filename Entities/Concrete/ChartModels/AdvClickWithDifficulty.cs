using Core.Entities;
using System;

namespace Entities.Concrete.ChartModels
{
    public class AdvClickWithDifficulty : DocumentDbEntity
    {
        public string ProjectId { get; set; }
        public int DifficultyLevel { get; set; }
        public long AdvClick { get; set; }
        public DateTime DateTime { get; set; }
    }
}