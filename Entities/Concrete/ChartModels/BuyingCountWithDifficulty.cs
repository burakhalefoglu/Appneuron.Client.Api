using Core.Entities;
using System;

namespace Entities.Concrete.ChartModels
{
    public class BuyingCountWithDifficulty : DocumentDbEntity
    {
        public string ProjectId { get; set; }
        public int DifficultyLevel { get; set; }
        public long BuyingCount { get; set; }
        public DateTime DateTime { get; set; }
    }
}