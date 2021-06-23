using Core.Entities;
using System;

namespace Entities.Concrete.ChartModels
{
    public class DailySessionWithDifficulty : DocumentDbEntity
    {
        public string ProjectId { get; set; }
        public DateTime DateTimePerThreeHour { get; set; }
        public int AvarageTimeSession { get; set; }
        public int DifficultyLevel { get; set; }
    }
}