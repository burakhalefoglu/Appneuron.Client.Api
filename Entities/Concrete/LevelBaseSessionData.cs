using Core.Entities;
using System;

namespace Entities.Concrete
{
    public class LevelBaseSessionData : DocumentDbEntity
    {
        public string ClientId { get; set; }
        public string ProjectId { get; set; }
        public string CustomerId { get; set; }
        public string LevelName { get; set; }
        public int DifficultyLevel { get; set; }
        public float SessionTimeMinute { get; set; }
        public DateTime SessionStartTime { get; set; }
        public DateTime SessionFinishTime { get; set; }
    }
}