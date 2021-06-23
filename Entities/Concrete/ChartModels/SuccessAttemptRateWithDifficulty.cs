using Core.Entities;
using System;

namespace Entities.Concrete.ChartModels
{
    public class SuccessAttemptRateWithDifficulty : DocumentDbEntity
    {
        public string ProjectId { get; set; }
        public int DifficultyLevel { get; set; }
        public long SuccessAttempt { get; set; }
        public DateTime DateTime { get; set; }
    }
}