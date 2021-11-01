using Core.Entities;
using System;

namespace Entities.Concrete
{ 
    public class AdvEvent : DocumentDbEntity
    {
        public string ClientId { get; set; }
        public string ProjectId { get; set; }
        public string CustomerId { get; set; }
        public string TrigersInlevelName { get; set; }
        public string AdvType { get; set; }
        public int DifficultyLevel { get; set; }
        public float InMinutes { get; set; }
        public DateTime TrigerdTime { get; set; }
    }
}