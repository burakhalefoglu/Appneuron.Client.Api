using Core.Entities;
using System;

namespace Entities.Concrete.ChartModels
{
    public class PlayerListByDayWithDifficulty : DocumentDbEntity
    {
        public string ProjectId { get; set; }
        public DateTime DateTime { get; set; }
        public string ClientId { get; set; }
        public int DifficultyLevel { get; set; }
    }
}