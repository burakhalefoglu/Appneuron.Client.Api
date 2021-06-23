using Core.Entities;
using System;

namespace Entities.Concrete
{
    public class BuyingEvent : DocumentDbEntity
    {
        public string ClientId { get; set; }
        public string ProjectID { get; set; }
        public string CustomerID { get; set; }
        public string TrigersInlevelName { get; set; }
        public string ProductType { get; set; }
        public int DifficultyLevel { get; set; }
        public float InWhatMinutes { get; set; }
        public DateTime TrigerdTime { get; set; }
    }
}