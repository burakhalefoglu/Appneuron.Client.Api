using Core.Entities;
using System;

namespace Entities.Dtos
{
    public class GameSessionEveryLoginDataDto : IDto
    {
        public string ClientId { get; set; }
        public DateTime SessionStartTime { get; set; }
        public DateTime SessionFinishTime { get; set; }
        public float SessionTimeMinute { get; set; }
    }
}
