using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class LevelbaseSessionWithPlayingTimeDto : IDto
    {
        public string ClientId { get; set; }
        public string levelName { get; set; }
        public DateTime SessionStartTime { get; set; }
        public float SessionTimeMinute { get; set; }
    }
}
