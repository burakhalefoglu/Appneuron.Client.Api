using Core.Entities;
using System;

namespace Entities.Concrete
{
    public class GameSessionEveryLoginData : DocumentDbEntity
    {
        public string ClientId { get; set; }
        public string ProjectId { get; set; }
        public string CustomerId { get; set; }
        public DateTime SessionStartTime { get; set; }
        public DateTime SessionFinishTime { get; set; }
        public float SessionTimeMinute { get; set; }
    }
}