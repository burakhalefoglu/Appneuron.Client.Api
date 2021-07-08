using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class LevelBaseSessionDataDto : IDto
    {
        public string ClientId { get; set; }
        public string levelName { get; set; }
        public int DifficultyLevel { get; set; }
        public float SessionTimeMinute { get; set; }
        public DateTime SessionStartTime { get; set; }
        public DateTime SessionFinishTime { get; set; }
    }
}
