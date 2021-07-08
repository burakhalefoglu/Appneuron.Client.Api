using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class EveryLoginLevelDataDto : IDto
    {
        public string ClientId { get; set; }
        public string Levelname { get; set; }
        public int LevelsDifficultylevel { get; set; }
        public int PlayingTime { get; set; }
        public int AverageScores { get; set; }
        public int TotalPowerUsage { get; set; }

    }
}
